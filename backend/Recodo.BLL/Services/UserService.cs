using AutoMapper;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.User;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Threading.Tasks;
using System;
using Recodo.Common.Security;

namespace Recodo.BLL.Services
{
    public class UserService : BaseService
    {

        public UserService(RecodoDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<UserDTO> CreateUser(UserRegisterDTO userRegisterDTO)
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
    }
}
