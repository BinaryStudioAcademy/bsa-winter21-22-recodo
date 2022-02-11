using AutoMapper;
using Recodo.BLL.JWT;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Auth;
using Recodo.Common.Dtos.Auth;
using Recodo.Common.Dtos.User;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recodo.BLL.Exceptions;
using Recodo.Common.Security;

namespace Recodo.BLL.Services
{
    public class AuthService : BaseService
    {
        private protected readonly JwtFactory _jwtFactory;
        public AuthService(RecodoDbContext context, IMapper mapper, JwtFactory jwtFactory) : base(context, mapper)
        {
            _jwtFactory = jwtFactory;
        }

        public async Task<AuthUserDTO>  Authorize(LoginUserDTO userDTO)
        {
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userDTO.Email);

            if(userEntity is null)
            {
                throw new NotFoundException(nameof(User));
            }

            if (!SecurityHelper.IsValidPassword(userEntity.Password, userDTO.Password, userEntity.Salt))
            {
                throw new InvalidUserNameOrPasswordException();
            }

            var token = await GenerateAccessToken(userEntity.Id, userEntity.UserName, userEntity.Email);
            var user = _mapper.Map<UserDTO>(userEntity);

            return new AuthUserDTO
            {
                Token = token,
                User = user
            };
        }

        public async Task<TokenDTO> GenerateAccessToken(int id, string userName, string userEmail)
        {
            string refreshToken = _jwtFactory.GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = id
            });

            await _context.SaveChangesAsync();

            string accessToken = await _jwtFactory.GenerateAccessToken(id, userName, userEmail);

            return new TokenDTO(accessToken, refreshToken);
        }

    }
}
