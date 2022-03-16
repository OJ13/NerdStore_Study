using System;
using System.Net;

namespace NSE.WebApp.MVC.Extensions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode StatusCode;
        public CustomHttpRequestException() {}

        public CustomHttpRequestException(string message, Exception innerExcpetion) : base(message, innerExcpetion) { }

        public CustomHttpRequestException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
