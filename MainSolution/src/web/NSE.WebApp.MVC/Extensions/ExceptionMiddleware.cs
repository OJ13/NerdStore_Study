using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptioAsync(httpContext, ex);
            }
        }

        private static void HandleRequestExceptioAsync(HttpContext httpContext, CustomHttpRequestException httpRequestException)
        {
            if (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                httpContext.Response.Redirect(location: $"/login?ReturnUlr={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int)httpRequestException.StatusCode;
        }
    }
}
