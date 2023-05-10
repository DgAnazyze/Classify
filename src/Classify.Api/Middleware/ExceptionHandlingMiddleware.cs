using Classify.Api.Models;
using Classify.Service.Commons.Exceptions;
using Microsoft.Extensions.FileProviders.Physical;

namespace Classify.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (CustomerException exception)
            {
                context.Response.StatusCode = exception.Code;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Code = exception.Code,
                    Message = exception.Message,
                });
            }
            catch (Exception ex) 
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new Response
                {
                    Code = 500,
                    Message = ex.Message,
                });
            }
        }
    }
}
