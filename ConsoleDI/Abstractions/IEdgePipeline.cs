using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDI.Abstractions
{
    //orchestrating one full read → cache → upload cycle
    internal interface IEdgePipeline
    {
        Task<bool> ProcessNextReadingAsync(CancellationToken cancellationToken);
    }
}
