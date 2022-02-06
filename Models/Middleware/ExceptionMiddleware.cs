using GestiuneCD.Models.Errors;
using System.Net;
using System.Text.Json;

namespace GestiuneCD.Models.Middleware
{
    /**
     */
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        /**
         */
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            //Used to process http requests
            _next = next;
        }

        /**
         */
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //If there is no exception, the request moves on
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _env.ApplicationName + ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}