using Recodo.Common.Dtos.Auth;

namespace Recodo.Common.Dtos.User
{
    public class AuthUserDTO
    {
        public UserDTO User { get; set; }
        public AccessTokenDTO Token { get; set; }
    }
}
