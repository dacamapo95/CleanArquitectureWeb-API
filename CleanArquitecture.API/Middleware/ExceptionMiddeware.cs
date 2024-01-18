using CleanArquitecture.API.ErrorMessages;
using CleanArquitecture.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace CleanArquitecture.API.Middleware;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            string result = string.Empty;

            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(
                        new CodeErrorException((int)HttpStatusCode.BadRequest, ex.Message, JsonConvert.SerializeObject(validationException.Errors)));
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            };

            if (string.IsNullOrEmpty(result))
                if (_environment.IsDevelopment())
                    result = JsonConvert.SerializeObject(new CodeErrorException(context.Response.StatusCode, ex.Message, ex.StackTrace));
                else result = JsonConvert.SerializeObject(new CodeErrorException(context.Response.StatusCode));

            await context.Response.WriteAsync(result);
        }
    }
}
