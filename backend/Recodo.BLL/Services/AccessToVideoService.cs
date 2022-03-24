using Recodo.BLL.Services.Abstract;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;
using Recodo.Common.Dtos.Access;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var unRegisteredUser = new AccessForUnregisteredUsersDTO
            {
                Email = email,
                VideoId = videoId
            };            
            var user = await _userService.GetUserByEmail(email);
            var accessedUser = await FindUnregisteredUser(unRegisteredUser);
            if(accessedUser == null)
            {
                if(user != null)
                {
                    var registeredUser = new AccessForRegisteredUsers
                    {
                        UserId = user.Id,
                        VideoId = videoId
                    };
                    var foundRegisteredUser = FindRegisteredUserAccess(_mapper.Map<AccessForRegisteredUsersDTO>(registeredUser));
                    if(foundRegisteredUser == null)
                    {
                        _context.AccessesForRegisteredUsers.Add(registeredUser);
                    }
                }
                else
                {
                    var unRegisteredUserEntity = _mapper.Map<AccessForUnregisteredUsers>(unRegisteredUser);
                    _context.AccessesForUnregisteredUsers.Add(unRegisteredUserEntity);
                }
            }
            else
            {
                await ChangeUserAccess(unRegisteredUser);
            }
            _context.SaveChanges();
        }

        public async Task ChangeUserAccess(AccessForUnregisteredUsersDTO unregisteredUser)
        {
            var foundUnregisteredUserDTO = await FindUnregisteredUser(unregisteredUser);
            var foundUnregisteredUser = _mapper.Map<AccessForUnregisteredUsers>(foundUnregisteredUserDTO);
            var foundUser = await _userService.GetUserByEmail(unregisteredUser.Email);
            var registeredUser = new AccessForRegisteredUsers
            {
                UserId = foundUser.Id,
                VideoId = unregisteredUser.VideoId
            };
            _context.AccessesForUnregisteredUsers.Remove(foundUnregisteredUser);
            _context.AccessesForRegisteredUsers.Add(registeredUser);
            _context.SaveChanges();
        }

        private async Task<AccessForUnregisteredUsersDTO> FindUnregisteredUser(AccessForUnregisteredUsersDTO unregisteredUser)
        {
            var accessedUsers = await _context.AccessesForUnregisteredUsers.Where(user => user.Email == unregisteredUser.Email).ToListAsync();
            var foundUser = accessedUsers.FirstOrDefault(user => user.VideoId == unregisteredUser.VideoId);
            return _mapper.Map<AccessForUnregisteredUsersDTO>(foundUser);
        }

        public async Task<AccessForRegisteredUsersDTO> FindRegisteredUserAccess(AccessForRegisteredUsersDTO accessedUserDTO)
        {
            var accessedUsers = await _context.AccessesForRegisteredUsers.Where(user => user.UserId == accessedUserDTO.UserId).ToListAsync();
            var foundUser = accessedUsers.FirstOrDefault(user => user.VideoId == accessedUserDTO.VideoId);
            return _mapper.Map<AccessForRegisteredUsersDTO>(foundUser);
        }
    }
}