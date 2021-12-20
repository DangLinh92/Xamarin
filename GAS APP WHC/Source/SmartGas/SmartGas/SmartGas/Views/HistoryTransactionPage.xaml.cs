using SmartGas.DataService;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartGas.Views
{
    /// <summary>
    /// Page to show the my order list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryTransactionPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryTransactionPage" /> class.
        /// </summary>
        public HistoryTransactionPage()
        {
            this.InitializeComponent();
            this.BindingContext = MyOrdersDataService.Instance.HistoryTransactionPageViewModel;
        }
    }
}