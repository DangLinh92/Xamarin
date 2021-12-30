using SmartGas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartGas.Views
{
    /// <summary>
    /// Page to add business details such as name, email address, and phone number.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistInPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistInOutPage" /> class.
        /// </summary>
        public RegistInPage()
        {
            this.InitializeComponent();
        }

        private void DatePicker_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            pickerButton.Text = string.Format("{0:dd/MM/yyyy}", e.NewValue);
        }

        private void DatePicker_Clicked(object sender, System.EventArgs e)
        {
            dateSX.IsOpen = true;
        }

        private void dateEx_OkButtonClicked(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            pickerButtonDateEx.Text = string.Format("{0:dd/MM/yyyy}", e.NewValue);
        }

        private void pickerButtonDateEx_Clicked(object sender, System.EventArgs e)
        {
            dateEx.IsOpen = true;
        }
        protected override void OnAppearing()
        {
            if (Application.Current.Resources.ContainsKey("QrCode"))
            {
                var qrCode = Application.Current.Resources["QrCode"] + "";
                (BindingContext as RegistInViewModel).QR_Code = qrCode;
            }
        }
    }
}