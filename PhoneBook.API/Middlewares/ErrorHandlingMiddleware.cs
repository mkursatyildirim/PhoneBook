using Newtonsoft.Json;
using System.Net;

namespace PhoneBook.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private Task HandleExceptions(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                _ => HttpStatusCode.InternalServerError
            };

            _logger.LogError(exception.Message);
            var result = JsonConvert.SerializeObject(new { error = "Beklenmedik bir hata oluştu." });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptions(context, exception);
            }
        }
    }
}
