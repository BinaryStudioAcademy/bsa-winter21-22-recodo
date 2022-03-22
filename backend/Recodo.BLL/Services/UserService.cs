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
using System.Linq;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services;

namespace Recodo.BLL.Services
{
    public class UserService : BaseService
    {
        private readonly TeamService _teamService;
        private readonly ImageService _imageService;
        private readonly IConfiguration _configuration;

        public UserService(ImageService imageService, TeamService teamService, RecodoDbContext context, IMapper mapper, IConfiguration configuration) : base(context, mapper)
        {
            _imageService = imageService;
            _configuration = configuration;
            _teamService = teamService;
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

            await _teamService.CreateTeam(userEntity);
            userEntity = _context.Users.Include(q => q.Teams)
                .FirstOrDefault(u => u.Email == userRegisterDTO.Email);

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

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Teams)
                    .ThenInclude(t => t.Users)
                .FirstOrDefaultAsync(p => p.Id == userId);

            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task AddToTeam(int userId, string authorEmail)
        {
            var author = await _context.Users.Where(q => q.Email == authorEmail).FirstOrDefaultAsync();
            if (author != null)
            {
                var team = await _context.Teams.Include(q => q.Users).Where(q => q.AuthorId == author.Id).FirstOrDefaultAsync();
                if (team != null)
                {
                    var user = await _context.Users.Where(q => q.Id == userId).FirstOrDefaultAsync();
                    team.Users.Add(user);

                    await _context.SaveChangesAsync();
                }
            }
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
