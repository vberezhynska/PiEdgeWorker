using ConsoleDI.Abstractions;
using Microsoft.Extensions.Logging;


namespace ConsoleDI.Services
{
    internal enum Action
    { 
        CoolDown,
        Heat
    }
    
    internal class TemperatureEventArgs : EventArgs
    { 
        public double Value { get; }
        public Action Action { get; }

        public TemperatureEventArgs(double temperature, Action action)
        {
            Value = temperature;
            Action = action;
        }
    }

    internal class TemperatureMonitorer(ILogger<TemperatureMonitorer> logger) : IMonitor
    {
        private bool isRaised = false;
        private const double maxAllowedTemperature = 23.5d;
        public event EventHandler<TemperatureEventArgs>? OnHighTemperature;
        public void EvaluateTemperature(double temperature)
        {
            if (temperature > maxAllowedTemperature && !isRaised)
            {
                logger.LogDebug($"Raising event.");
                var e = new TemperatureEventArgs(temperature, Action.CoolDown);
                OnLHighTemperatureDetected(e);
                isRaised = true;
            }

            if (temperature < maxAllowedTemperature && isRaised)
            {
                logger.LogInformation($"Temperatre has been normalized. Resetting moritorer.");
                isRaised = false;
            }
        }

        private void OnLHighTemperatureDetected(TemperatureEventArgs e)
        {
            OnHighTemperature?.Invoke(this, e);
        }
    }
}