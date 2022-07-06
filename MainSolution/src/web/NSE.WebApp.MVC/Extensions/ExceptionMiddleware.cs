using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
//using Refit;

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
                HandleRequestExceptioAsync(httpContext, ex.StatusCode);
            }
            //catch (ValidationApiException ex)
            //{
            //    HandleRequestExceptioAsync(httpContext, ex.StatusCode);
            //}
            //catch (ApiException ex)
            //{
            //    HandleRequestExceptioAsync(httpContext, ex.StatusCode);
            //}
        }

        private static void HandleRequestExceptioAsync(HttpContext httpContext, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                httpContext.Response.Redirect(location: $"/login?ReturnUlr={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int)statusCode;
        }
    }
}
