using ConsoleDI.Abstractions;
using Microsoft.Extensions.Logging;

namespace ConsoleDI.Services
{
    internal class EdgePipeline(
        ISensorReader sensorReaded,
        ILocalCache cache,
        ICloudUploader uploader,
        IMonitor monitor,
        ILogger<EdgePipeline> logger
        ) : IEdgePipeline
    {
        public async Task<bool> ProcessNextReadingAsync(CancellationToken cancellationToken)
        {
            double? temperature = await sensorReaded.ReadTemperatureAsync(cancellationToken);
            if (temperature is null)
                return false;

            cache.Save(temperature.Value);
            monitor.EvaluateTemperature(temperature.Value);
            logger.LogInformation($"Cached readinglocaly. Total count: {cache.Count}");
            await uploader.UploadAsync(temperature.Value, cancellationToken);

            return true;
        }
    }
}