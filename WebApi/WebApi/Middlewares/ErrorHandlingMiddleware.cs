using System.Net;
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
            await context.Response.WriteAsync(new
            {
                StatusCode = apiEx.StatusCode,
                Message = apiEx.Message
            }.ToString());
        }
        catch (Exception ex) 
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred."
            }.ToString());
        }
    }
}
