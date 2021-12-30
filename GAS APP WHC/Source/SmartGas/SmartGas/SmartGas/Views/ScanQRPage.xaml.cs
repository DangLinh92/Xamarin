using SmartGas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartGas.Views
{
    /// <summary>
    /// Page to show no videos
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanQRPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanQRPage" /> class.
        /// </summary>
        public ScanQRPage()
        {
            this.InitializeComponent();
            this.BindingContext = ScanQRViewModel.BindingContext;
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                scanResultText.Text = result.Text;
            });
        }
    }
}