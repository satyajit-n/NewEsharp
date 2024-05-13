using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BuildingBlock.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                Console.WriteLine("After ================>" + httpContext.Response);
            }
            catch (Exception ex)
            {
                logger.LogError("ex: " + ex.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var route = httpContext.Request.Path.ToString();

                // Check if "/api/" is present and remove it
                if (route.Contains("/api/", StringComparison.OrdinalIgnoreCase))
                {
                    route = route[(route.IndexOf("/api/") + 5)..]; // +5 to skip "/api/"
                }
                // Check if "/auth/" is present and remove it
                else if (route.Contains("/auth/", StringComparison.OrdinalIgnoreCase))
                {
                    route = route[(route.IndexOf("/auth/") + 6)..]; // +6 to skip "/auth/"
                }

                // Remove any leading slashes
                route = route.TrimStart('/');

                // Remove all slashes from the remaining route string
                route = route.Replace("/", " ");

                var displayMessage = new
                {
                    displayMessage = "Something Went Wrong!",
                    isSuccess = false,
                    supportMessage = new
                    {
                        Route = $"{route} : Internal Server Error",
                        ex.Message,
                        ex.StackTrace
                    }
                };
            }
        }
    }
}
