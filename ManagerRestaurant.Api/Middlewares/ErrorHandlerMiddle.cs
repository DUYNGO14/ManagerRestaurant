
using ManagerRestaurant.Domain.Exceptions;

namespace ManagerRestaurant.API.Middlewares
{
    public class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
                logger.LogWarning(ex.Message);
            }
            catch (ForbidenException ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden!");
            }
            catch (Exception ex) 
            { 
                logger.LogError(ex,ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong!");
            }
        }
    }
}
