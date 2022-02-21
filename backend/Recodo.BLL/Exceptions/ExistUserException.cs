using System;


namespace Recodo.BLL.Exceptions
{
    public class ExistUserException : Exception
    {
        public ExistUserException(string email)
            : base($"User with {email} already exists.")
        {

        }
    }
}
