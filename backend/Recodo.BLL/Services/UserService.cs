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
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services;

namespace Recodo.BLL.Services
{
    public class UserService : BaseService
    {
        private readonly IConfiguration _configuration;

        public UserService(RecodoDbContext context, IMapper mapper, IConfiguration configuration) : base(context, mapper)
        {
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

            userEntity.WorkspaceName = userDto.WorkspaceName ?? userDto.WorkspaceName;


            if (avatar != null)
            {
                using (var client = new HttpClient())
                {
                    var url = "https://upload.gyazo.com/api/upload?access_token=" + _configuration["GyazoKey"];

                    using (var memoryStream = new MemoryStream())
                    {
                        await avatar.CopyToAsync(memoryStream);
                        var avatarImage = memoryStream.ToArray();

                        ByteArrayContent byteContent = new ByteArrayContent(avatarImage);
                        var multipartContent = new MultipartFormDataContent();
                        multipartContent.Add(byteContent, "imagedata", "imagedata");

                        var response = await client.PostAsync(url, multipartContent);
                        var stringResponse = await response.Content.ReadAsStringAsync();

                        AvatarDTO json = JsonSerializer.Deserialize<AvatarDTO>(stringResponse);
                        userEntity.AvatarLink = json.thumb_url;
                    }

                    //userDto.Avatar
                }
            }

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPassword(UpdateUserDTO userDto)
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

        public async Task ResetPassword(UpdateUserDTO userDto)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (userEntity == null) { return; }

            string newPassword = Guid.NewGuid().ToString().Substring(0, 10);
            var salt = SecurityHelper.GetRandomBytes();
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(newPassword, salt);

            string message = "Temp password: " + newPassword;
            await EmailService.SendEmailAsync(userEntity.Email, "New Password", message);
        }

        public async Task DeleteUser(UpdateUserDTO userDto)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
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
