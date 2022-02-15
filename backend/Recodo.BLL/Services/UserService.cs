﻿using AutoMapper;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Auth;
using Recodo.Common.Dtos.User;
using Recodo.Common.Security;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class UserService : BaseService
    {

        public UserService(RecodoDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<UserDTO> CreateUser(NewUserDTO userRegisterDTO)
        {
            var userEntity = _mapper.Map<User>(userRegisterDTO);
            userEntity.CreatedAt = DateTime.UtcNow;

            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(userRegisterDTO.Password, salt);

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<UserDTO> CreateGoogleUser(ExternalAuthDto userRegisterDTO,
            GoogleJsonWebSignature.Payload payload)
        {
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == payload.Email);

            if (userEntity == null)
            {
                var newUserDTO = new NewUserDTO
                {
                    Email = payload.Email,
                    UserName = payload.Name,
                    Password = userRegisterDTO.IdToken
                };

                return await CreateUser(newUserDTO);
            }
            else
            {
                return _mapper.Map<UserDTO>(userEntity);
            }
        }
    }
}
