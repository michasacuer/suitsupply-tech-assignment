using System.Net;

namespace Suitsupply.Alteration.Api.Middlewares;

public class ExceptionWrapperMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionWrapperMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(message);
    }
}