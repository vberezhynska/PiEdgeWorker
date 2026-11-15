using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDI.Abstractions
{
    internal interface ILocalCache
    {
        int Count { get; }
        void Save(double temperature);
    }
}
