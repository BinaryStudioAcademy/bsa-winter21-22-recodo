using AutoMapper;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Recodo.BLL.Exceptions;
using Recodo.BLL.JWT;
using Recodo.BLL.Services.Abstract;
using Recodo.Common.Dtos.Auth;
using Recodo.Common.Dtos.User;
using Recodo.Common.Security;
using Recodo.DAL.Context;
using Recodo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class AuthService : BaseService
    {
        private protected readonly JwtFactory _jwtFactory;
        private readonly IConfiguration _configuration;

        public AuthService(RecodoDbContext context, IMapper mapper, JwtFactory jwtFactory, IConfiguration configuration)
            : base(context, mapper)
        {
            _jwtFactory = jwtFactory;
            _configuration = configuration;
        }

        public async Task<AuthUserDTO> Authorize(LoginUserDTO userDTO)
        {
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userDTO.Email);

            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User));
            }

            if (!SecurityHelper.IsValidPassword(userEntity.Password, userDTO.Password, userEntity.Salt))
            {
                throw new InvalidUserNameOrPasswordException();
            }

            var token = await GenerateAccessToken(userEntity.Id, userEntity.WorkspaceName, userEntity.Email);
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

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            try
            {
                string clientId = _configuration["GoogleClientId"];
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { clientId }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
