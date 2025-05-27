using Microsoft.AspNetCore.Diagnostics;

namespace ContactSystem.Server.ActionHelpers;

public class AppExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string errorMessage) = exception switch
        {
            NullReferenceException nullReferenceException => (400, nullReferenceException.Message),
            ArgumentException argumentException => (400, argumentException.Message),
            Exception defaultException => (400, defaultException.Message),
            _ => default
        };

        if (statusCode == default)
        {
            return false;
        }
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorMessage);

        return true;
    }
}
