using ConsoleDI.Abstractions;

namespace ConsoleDI.Services
{
    internal class SensorGenerator : ISensorReader
    {
        public async Task<double?> ReadTemperatureAsync(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500, cancellationToken);
                var rawTemp = (Random.Shared.NextDouble() * 200.00) - 100.0;
                return Math.Round(rawTemp, 2);
            }

            return null;
        }
    }
}
