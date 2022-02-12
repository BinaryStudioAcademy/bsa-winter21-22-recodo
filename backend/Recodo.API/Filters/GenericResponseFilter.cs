using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recodo.Common.Dtos.Response;

namespace Recodo.API.Filters
{
    public class GenericResponseFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Response.ContentType == "validation-errors-type")
            {
                context.HttpContext.Response.ContentType = "application/json";
                return;
            }

            context.HttpContext.Response.ContentType = "application/json";
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = new ResponseDTO { Data = objectResult.Value };
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
