using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Recodo.BLL.Exceptions;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Auth;
using Recodo.Common.Dtos.User;
using Recodo.Common.Security;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services;

namespace Recodo.BLL.Services
{
    public class UserService : BaseService
    {
        private readonly ImageService _imageService;
        private readonly IConfiguration _configuration;

        public UserService(ImageService imageService, RecodoDbContext context, IMapper mapper, IConfiguration configuration) : base(context, mapper)
        {
            _imageService = imageService;
            _configuration = configuration;
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

        public async Task UpdateUser(UpdateUserDTO userDto, IFormFile avatar)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (userEntity == null) { return; }

            userEntity.WorkspaceName = userDto.WorkspaceName ?? userEntity.WorkspaceName;
            if (avatar != null)
            {
                userEntity.AvatarLink = await _imageService.UploadToGyazo(avatar, _configuration["GyazoKey"]);
            }

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPasswordEmail(UpdateUserDTO userDto)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (userEntity == null) { return; }

            if (!SecurityHelper.IsValidPassword(userEntity.Password, userDto.PasswordCurrent, userEntity.Salt))
            {
                throw new InvalidUserNameOrPasswordException();
            }

            userEntity.Email = userDto.Email ?? userEntity.Email;
            if (!String.IsNullOrWhiteSpace(userDto.PasswordNew) &&
                !String.IsNullOrWhiteSpace(userDto.PasswordCurrent))
            {
                var salt = SecurityHelper.GetRandomBytes();
                userEntity.Salt = Convert.ToBase64String(salt);
                userEntity.Password = SecurityHelper.HashPassword(userDto.PasswordNew, salt);
            }

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPassword(int userId)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userEntity == null) { return; }

            string newPassword = Guid.NewGuid().ToString().Substring(0, 10);
            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(newPassword, salt);

            string message = "Temp password: " + newPassword;
            await EmailService.SendEmailAsync(userEntity.Email, "New Password", message, _configuration);
        }

        public async Task DeleteUser(int userId)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userEntity == null) { return; }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
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
                    Password = userRegisterDTO.IdToken,
                    WorkspaceName = userRegisterDTO.WorkspaceName
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
