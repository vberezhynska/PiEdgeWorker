using ConsoleDI.Abstractions;
using Microsoft.Extensions.Logging;

namespace ConsoleDI.Services
{
    internal class ConsoleCloudUploader(ILogger<ConsoleCloudUploader> logger) : ICloudUploader
    {
        public async Task UploadAsync(double temperature, CancellationToken cancellationToken)
        {
            var corellationId = Guid.NewGuid();
            logger.LogInformation(
                $"[Thread {Environment.CurrentManagedThreadId}][CorellationId {corellationId}] Uploading {temperature}°C to cloud.");

            //simulate network
            await Task.Delay(1000, cancellationToken);

            logger.LogInformation($"[Thread {Environment.CurrentManagedThreadId}][CorellationId {corellationId}] Unload completed.");
        }
    }
}
