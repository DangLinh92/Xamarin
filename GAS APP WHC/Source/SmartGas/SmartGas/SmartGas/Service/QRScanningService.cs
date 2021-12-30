using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartGas.Service
{
    public static class QRScanningService
    {
        public static async Task<string> ScanQrCode()
        {
            try
            {
                var scanner = DependencyService.Get<IQrScaninngService>();
                var result = await scanner.ScanAsync();
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
