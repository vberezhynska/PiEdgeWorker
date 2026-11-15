using ConsoleDI.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleDI.Services
{
    internal class TemperatureHandler(ILogger<TemperatureHandler> logger, IMonitor monitor) 
        : IHostedService
    {
        public void OnHighTemperature(object? publisher, TemperatureEventArgs e)
        {
            logger.LogWarning($"[{nameof(TemperatureHandler)}] {e.Action}. Current temperature is too high {e.Value}");
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Starting service {nameof(TemperatureHandler)}...");
            monitor.OnHighTemperature += OnHighTemperature;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Shooting down service {nameof(TemperatureHandler)}...");
            monitor.OnHighTemperature -= OnHighTemperature;
            return Task.CompletedTask;
        }
    }
}
