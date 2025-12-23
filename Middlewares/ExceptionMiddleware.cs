namespace Projexor.Middlewares;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext http)
    {
        try
        {
            await next(http);
        }

        catch (OperationCanceledException) { }

        catch (Exception ex)
        {
            logger.LogError(ex, "Exception Error.");

            http.Response.StatusCode = StatusCodes.Status500InternalServerError;
            http.Response.ContentType = "application/json";

            var response = new { message = "Internal server error. Try again later." };

            await http.Response.WriteAsJsonAsync(response);
        }
    }
}