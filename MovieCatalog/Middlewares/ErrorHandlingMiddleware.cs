using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace MovieCatalog.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado capturado pelo middleware global.");

                if (ex is DbUpdateException dbEx &&
                  dbEx.InnerException?.Message.Contains("Duplicate entry") == true)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    context.Response.ContentType = "application/json";

                    var conflictResponse = new
                    {
                        message = "Já existe um registro com os dados informados.",
                        status = StatusCodes.Status409Conflict
                    };

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(conflictResponse)
                    );

                    return; 
                }

                var statusCode = context.Response.StatusCode;

                if (statusCode == 200)
                {
                    statusCode = (int)HttpStatusCode.InternalServerError;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;


                var errorResponse = new
                {
                    message = "Ocorreu um erro inesperado no servidor. Por favor, tente novamente mais tarde.",
                    status = statusCode,
                    detail = ex.Message
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
