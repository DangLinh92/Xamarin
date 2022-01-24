using SmartGas.DataService;
using SmartGas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartGas.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistReturnGasPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsPage" /> class.
        /// </summary>
        public RegistReturnGasPage()
        {
            this.InitializeComponent();
            this.BindingContext = new RegistReturnGasViewModel();
        }

        protected override void OnAppearing()
        {
            if (Application.Current.Resources.ContainsKey("QrCode"))
            {
                var qrCode = Application.Current.Resources["QrCode"] + "";
                (BindingContext as RegistReturnGasViewModel).QR_Code = qrCode;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (Application.Current.Resources.ContainsKey("QrCode"))
            {
                Application.Current.Resources["QrCode"] = "";
            }
            return base.OnBackButtonPressed();
        }
    }
}