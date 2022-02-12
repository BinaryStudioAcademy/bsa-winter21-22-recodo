using System;

namespace Recodo.BLL.Exceptions
{
    public class InvalidUsernameOrPasswordException : Exception
    {
        public InvalidUsernameOrPasswordException()
            : base("Invalid username or password.")
        {

        }
    }
}
