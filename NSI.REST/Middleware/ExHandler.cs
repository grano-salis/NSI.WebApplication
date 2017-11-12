using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSI.DC.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NSI.REST.Middleware
{
    public class ExHandler
    {
        private readonly RequestDelegate _next;

        public ExHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NSIException ex)
            {
                var result = JsonConvert.SerializeObject(new NSI.DC.Response.NSIResponse<string>
                {
                    Message = ex.Message
                });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                var result = JsonConvert.SerializeObject(new NSI.DC.Response.NSIResponse<string>
                {
                    Message = "An error occured while processing your request. Please contact support."
                });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(result);
            }
        }
    }

    public static class Extensions
    {
        public static IApplicationBuilder UseExHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExHandler>();
        }

        public static IApplicationBuilder UseAuthHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthHandler>();
        }
    }
}
