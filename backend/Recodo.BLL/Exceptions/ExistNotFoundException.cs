using System;

namespace Recodo.BLL.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email)
         : base($"User with email {email} not exists.")
        { }
    }
}
