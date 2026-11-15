using ConsoleDI.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleDI
{
    internal class Worker(
            IServiceScopeFactory scopeFactory,
            IHostApplicationLifetime hostLifetime,
            ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"PiEdgeWorker started on Thread {Environment.CurrentManagedThreadId}.");
            while (!cancellationToken.IsCancellationRequested)
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var pipeline = scope.ServiceProvider.GetRequiredService<IEdgePipeline>();

                bool shouldContinue = await pipeline.ProcessNextReadingAsync(cancellationToken);
                if (!shouldContinue)
                {
                    hostLifetime.StopApplication();
                    break;
                }
            }

            logger.LogInformation("PiEdgeWorker stopped.");
        }
    }
}