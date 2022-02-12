using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recodo.API.Extensions;
using Recodo.Common.Dtos.Response;
using System;

namespace Recodo.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var (statusCode, errorCode) = context.Exception.ParseException();
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;

            context.Result = new JsonResult(new ResponseDTO
            {
                Data = $"Code {(int)errorCode} ({errorCode})",
                IsError = true,
                ExceptionMessage = context.Exception.Message,
            });
        }
    }
}
