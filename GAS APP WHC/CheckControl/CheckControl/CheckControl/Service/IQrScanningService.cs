using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CheckControl.Service
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
