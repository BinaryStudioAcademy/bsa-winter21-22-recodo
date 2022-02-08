using System;

namespace Recodo.BLL.Exceptions
{
    public class ExpiredRefreshTokenException : Exception
    {
        public ExpiredRefreshTokenException() : base("Refresh token expired.") { }
    }
}
