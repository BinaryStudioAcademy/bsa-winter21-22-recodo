
namespace Recodo.Common.Auth
{
    public class AccessToken
    {
        public string Token { get; }
        public AccessToken(string token)
        {
            Token = token;
        }
    }
}
