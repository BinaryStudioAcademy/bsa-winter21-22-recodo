using AutoMapper;
using Google.Apis.Auth;
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
using System.Linq;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services;

namespace Recodo.BLL.Services
{
    public class UserService : BaseService
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public UserService(RecodoDbContext context, IMapper mapper, AuthService authService,
            IConfiguration configuration)
            : base(context, mapper)
        {
            _authService = authService;
            _configuration = configuration;
        }

        public async Task<UserDTO> CreateUser(NewUserDTO userRegisterDTO)
        {
            var userEntity = _mapper.Map<User>(userRegisterDTO);
            userEntity.CreatedAt = DateTime.UtcNow;

            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(userRegisterDTO.Password, salt);


            var existUser = _context.Users.FirstOrDefault(u => u.Email == userRegisterDTO.Email);
            if (existUser != null)
            {
                throw new ExistUserException(userRegisterDTO.Email);
            }

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
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

        public async Task ResetPassword(string email)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existUser == null)
            {
                throw new UserNotFoundException(email);
            }

            var token = await _authService.GenerateAccessToken(existUser.Id, existUser.Email, existUser.Email);
            string accessToken = token.AccessToken;
            string clientHost = _configuration["ClientHost"];
            string url = $"{clientHost}/reset-finish?token={accessToken}";

            await EmailService.SendEmailAsync(email, "New Password", url, _configuration);
        }

        public async Task<LoginUserDTO> ResetPasswordFinish(string email, string newPass)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existUser == null)
            {
                throw new UserNotFoundException(email);
            }

            var salt = SecurityHelper.GetRandomBytes();
            existUser.Salt = Convert.ToBase64String(salt);
            existUser.Password = SecurityHelper.HashPassword(newPass, salt);

            _context.Users.Update(existUser);
            await _context.SaveChangesAsync();

            return new LoginUserDTO { Email = existUser.Email, Password = newPass };
        }
    }
}
