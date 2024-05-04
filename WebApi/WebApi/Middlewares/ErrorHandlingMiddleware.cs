using System.Net;
using System.Text.Json;
using WebApi.Exceptions;

namespace WebApi.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException apiEx)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)apiEx.StatusCode;
            
            var errorResponse = JsonSerializer.Serialize(new
            {
                apiEx.StatusCode,
                apiEx.Message
            });
            await context.Response.WriteAsync(errorResponse);
        }
        catch (Exception ex) 
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred."
            }.ToString()!);
        }
    }
}
