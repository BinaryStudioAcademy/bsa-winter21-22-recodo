using System;

namespace Recodo.BLL.Exceptions
{
    public sealed class InvalidUserNameOrPasswordException : Exception
    {
        public InvalidUserNameOrPasswordException() 
            : base("Invalid username or password.") 
        {

        }
    }
}
