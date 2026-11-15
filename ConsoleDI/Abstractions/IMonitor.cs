using ConsoleDI.Services;
namespace ConsoleDI.Abstractions
{
    internal interface IMonitor
    {
        public event EventHandler<TemperatureEventArgs>? OnHighTemperature;
        void EvaluateTemperature(double temperature);
    }
}
