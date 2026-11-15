namespace ConsoleDI.Abstractions
{
    internal interface ISensorReader
    {
        Task<double?> ReadTemperatureAsync(CancellationToken cancelationToken);
    }
}
