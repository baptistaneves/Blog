using Blog.Api.Contracts.Common;
using System.Net;
using System.Text.Json;

namespace Blog.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(IHostEnvironment environment,
                                           ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            //RequestDelegate is what's next, what is coming up next in the middleware pipeline
            //We need ILogger to display our exception in the terminal
            //IHostEnvironment to check what environment we're running

            _environment = environment;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _next(context);
            }
            catch (Exception ex)
            {
                //To log the exception in the terminal
                _logger.LogError(ex.Message);

                //Set the Exception in the HttpContextResponse
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //Create the response
                var response = _environment.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                //Serialize the response to json camelcase
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var json = JsonSerializer.Serialize(response, options);

                //Write the Exception to the the HttpContextResponse
                await context.Response.WriteAsync(json);
            }
        }
    }
}
