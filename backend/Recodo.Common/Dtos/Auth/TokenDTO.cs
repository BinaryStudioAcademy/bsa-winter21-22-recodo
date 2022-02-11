using Recodo.Common.Auth;

namespace Recodo.Common.Dtos.Auth
{
    public class TokenDTO
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public TokenDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
