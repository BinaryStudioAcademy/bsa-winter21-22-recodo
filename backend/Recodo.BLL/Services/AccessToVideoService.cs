using Recodo.BLL.Services.Abstract;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;
using Recodo.Common.Dtos.Access;

namespace Recodo.BLL.Services
{
    public class AccessToVideoService : BaseService
    {
        private UserService _userService;
        public AccessToVideoService(RecodoDbContext context, IMapper mapper, UserService userService)
            : base(context, mapper)
        {
            _userService = userService;
        }

        public async Task AddUserAccess(string email, int videoId)
        {
            var user = await _userService.GetUserByEmail(email);
            if(user != null)
            {
                var registeredUser = new AccessForRegisteredUsers
                {
                    UserId = user.Id,
                    VideoId = videoId
                };
                _context.AccessesForRegisteredUsers.Add(registeredUser);
            }
            else
            {
                var unRegisteredUser = new AccessForUnregisteredUsers
                {
                    Email = email,
                    VideoId = videoId
                };
                _context.AccessesForUnregisteredUsers.Add(unRegisteredUser);
            }
            _context.SaveChanges();
        }

        public async Task ChangeUserAccess(string email, int videoId)
        {
            var foundUnregisteredUserDTO = await FindUnregisteredUser(email);
            var foundUnregisteredUser = _mapper.Map<AccessForUnregisteredUsers>(foundUnregisteredUserDTO);
            var foundUser = await _userService.GetUserByEmail(email);
            var registeredUser = new AccessForRegisteredUsers
            {
                UserId = foundUser.Id,
                VideoId = videoId
            };
            _context.AccessesForUnregisteredUsers.Remove(foundUnregisteredUser);
            _context.AccessesForRegisteredUsers.Add(registeredUser);
            _context.SaveChanges();
        }

        private async Task<AccessForUnregisteredUsersDTO> FindUnregisteredUser(string email)
        {
            var foundUser = await _context.AccessesForUnregisteredUsers.FindAsync(email);
            return _mapper.Map<AccessForUnregisteredUsersDTO>(foundUser);
        }

        public async Task<AccessForRegisteredUsersDTO> FindRegisteredUserAccess(AccessForRegisteredUsersDTO accessedUserDTO)
        {
            var accessedUser = _mapper.Map<AccessForRegisteredUsers>(accessedUserDTO);
            var foundUser = await _context.AccessesForRegisteredUsers.FindAsync(accessedUser);
            return _mapper.Map<AccessForRegisteredUsersDTO>(foundUser);
        }
    }
}