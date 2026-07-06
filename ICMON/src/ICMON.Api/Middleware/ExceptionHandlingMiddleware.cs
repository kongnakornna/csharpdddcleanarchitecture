using System.Net;
using System.Text.Json;
using FluentValidation;
using ICMON.Domain;

namespace ICMON.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An error occurred while processing your request.";

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                message = string.Join("; ", validationEx.Errors.Select(e => e.ErrorMessage));
                break;

            case DomainException domainEx:
                statusCode = HttpStatusCode.BadRequest;
                message = domainEx.Message;
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                message = "Unauthorized access.";
                break;

            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = "Resource not found.";
                break;
        }

        _logger.LogError(exception, "Error: {Message}", exception.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            error = message,
            statusCode = (int)statusCode
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
