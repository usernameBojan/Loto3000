using Loto3000.Domain.Exceptions;
using Serilog;
using System.Diagnostics;

namespace Loto3000.Middlewares
{
    public class Logger
    {
        private readonly RequestDelegate next;

        public Logger(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var stopwatch = new Stopwatch();
                Log.Logger.Information("Request - ");

                stopwatch.Start();
                await next(context);
                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > 500)
                {
                    Log.Logger.Warning("Request took {duration}", stopwatch.ElapsedMilliseconds);
                }

                Log.Logger.Information("Response");
            }
            catch (NotFoundException ex)
            {
                Log.Logger.Warning("An exception occured", ex, $"{ex}");
                throw;
            }
            catch (NotAllowedException ex)
            {
                Log.Logger.Warning("An exception occured", ex);
                throw;
            }
            catch (ValidationException ex)
            {
                Log.Logger.Warning("An exception occured", ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("An exception occured", ex);
                throw;
            }
        }
    }
}