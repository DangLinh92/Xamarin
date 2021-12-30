using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartGas.Service
{
    public interface IQrScaninngService
    {
        Task<string> ScanAsync();
    }
}
