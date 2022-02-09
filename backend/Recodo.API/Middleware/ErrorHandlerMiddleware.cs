using Microsoft.AspNetCore.Http;
using Recodo.BLL.Exceptions;
using System;
using System.Net;
using System.Text.Json;
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

                response.StatusCode = error switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    InvalidUserNameOrPasswordException => (int)HttpStatusCode.Unauthorized,
                    ExpiredRefreshTokenException => (int)HttpStatusCode.Unauthorized,
                    InvalidTokenException => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(error.Message));
            }
        }
    }
}
