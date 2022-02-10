using AutoMapper;
using Recodo.BLL.JWT;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Auth;
using Recodo.Common.Dtos.Auth;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class AuthService : BaseService
    {
        private protected readonly JwtFactory _jwtFactory;
        public AuthService(RecodoDbContext context, IMapper mapper, JwtFactory jwtFactory) : base(context, mapper)
        {
            this._jwtFactory = jwtFactory;
        }

        public async Task<AccessTokenDTO> GenerateAccessToken(int id, string userName, string userEmail)
        {
            string refreshToken = _jwtFactory.GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = id
            });

            await _context.SaveChangesAsync();

            AccessToken accessToken = await _jwtFactory.GenerateAccessToken(id, userName, userEmail);

            return new AccessTokenDTO(accessToken,refreshToken);
        }

    }
}
