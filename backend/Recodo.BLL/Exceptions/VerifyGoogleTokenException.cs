using System;

namespace Recodo.BLL.Exceptions
{
    public class VerifyGoogleTokenException : Exception
    {
        public VerifyGoogleTokenException()
            : base($"Invalid google token")
        {

        }
    }
}
