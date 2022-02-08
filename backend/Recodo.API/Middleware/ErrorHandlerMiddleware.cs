using Microsoft.AspNetCore.Http;
using Recodo.BLL.Exceptions;
using Recodo.Common.Error;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Recodo.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case InvalidUserNameOrPasswordException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ExpiredRefreshTokenException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case InvalidTokenException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = error.Message
                }.ToString());
            }
        }
    }
}
