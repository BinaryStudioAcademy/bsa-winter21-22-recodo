using AutoMapper;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
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

namespace Recodo.BLL.Services
{
    public class UserService : BaseService
    {
        private readonly TeamService _teamService;

        public UserService(RecodoDbContext context, IMapper mapper, TeamService teamService) : base(context, mapper)
        {
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
