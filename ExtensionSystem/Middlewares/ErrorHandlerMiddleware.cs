using MyMessageApp.Core.Exceptions;
using MyMessageApp.Core.Models;
using System.Net;

namespace MyMessageApp.Middlewares
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
            catch (HttpStatusCodeException ex)
            {
                context.Response.StatusCode = (int)ex.StatusCode;

                context.Response.ContentType = ex.ContentType;

                await context.Response.WriteAsync(new ErrorResultModel
                {
                    Message = ex.Message,
                }.ToString());
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(new ErrorResultModel
                {
                    Message = ex.Message,
                }.ToString());
            }
        }
    }
}