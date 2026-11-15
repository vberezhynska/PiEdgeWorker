using ConsoleDI.Abstractions;
using System.Collections.Concurrent;

namespace ConsoleDI.Services
{
    internal class InMemoryCache : ILocalCache
    {
        private readonly ConcurrentQueue<double> _temperatureList = new();

        public int Count => _temperatureList.Count;
        public double Last => _temperatureList.Last();
        public void Save(double temperature)
        {
            _temperatureList.Enqueue(temperature);
        }
    }
}
