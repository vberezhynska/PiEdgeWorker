using ConsoleDI.Abstractions;
using Microsoft.Extensions.Logging;

namespace ConsoleDI.Services
{
    internal class ConsoleSensorReader(ILogger<ConsoleSensorReader> logger) : ISensorReader
    {       
        public async Task<double?> ReadTemperatureAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Enter temperature in °C (or 'exit' to quit): ");
                string? input = String.Empty;
                try
                {
                    Task<string?> readTask = Task.Run(() => Console.ReadLine(), cancellationToken);
                    Task<string?> cancellableReadTask = readTask.WaitAsync(cancellationToken); // handle Ctrl+C
                    input = await cancellableReadTask;
                    // one line:
                    //input = await Task.Run(Console.ReadLine, cancellationToken).WaitAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return null;
                }
                catch (Exception)
                {
                    throw;
                }

                if (String.Equals(input?.Trim(), "exit", StringComparison.OrdinalIgnoreCase) )
                {
                    logger.LogInformation("Exit command received from console.");
                    return null;
                }

                if (double.TryParse(input, out double temperature))
                {
                    return temperature;
                }

                Console.WriteLine("Invalid input. Please enter valid real number.");
            }

            return null;
        }
    }
}