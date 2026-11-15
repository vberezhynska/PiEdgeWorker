using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDI.Abstractions
{
    internal interface ICloudUploader
    {
        Task UploadAsync(double temperature, CancellationToken cancellationToken);
    }
}
