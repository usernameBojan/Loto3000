using Loto3000.Domain.Exceptions;

namespace Loto3000.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            catch (NotAllowedException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            catch (ValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}