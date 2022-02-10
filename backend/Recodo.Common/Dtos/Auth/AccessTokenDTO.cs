using Recodo.Common.Auth;

namespace Recodo.Common.Dtos.Auth
{
    public class AccessTokenDTO
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }

        public AccessTokenDTO(AccessToken accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
