using Application.Exceptions;
using Domain.Exceptions.Base;
using YANLib;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Web.Middlerware;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContent, Exception exception)
    {
        httpContent.Response.ContentType = "application/json";

        httpContent.Response.StatusCode = exception switch
        {
            BadRequestException or ValidationException => Status400BadRequest,
            NotFoundException => Status404NotFound,
            _ => Status500InternalServerError
        };

        var errors = Array.Empty<ApiError>();

        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors
                .SelectMany(p => p.Value, (p, value) => new ApiError(p.Key, value))
                .ToArray();
        }

        var response = new
        {
            status = httpContent.Response.StatusCode,
            message = exception.Message,
            errors
        };

        await httpContent.Response.WriteAsync(response.CamelSerialize());
    }

    private record ApiError(string PropertyName, string ErrorMessage);
}
