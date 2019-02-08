using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AgilePartner.CDC.Kata.Bar.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private static IReadOnlyDictionary<Type, HttpStatusCode> exceptionToHttpStatus = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(NotAuthorizedException), HttpStatusCode.Unauthorized },
            { typeof(Exception), HttpStatusCode.InternalServerError }
        };

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)exceptionToHttpStatus[exception.GetType()];

            return context.Response.WriteAsync(new
            {
                context.Response.StatusCode
            }.ToString());
        }
    }
}