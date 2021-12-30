using SmartGas.DataService;
using SmartGas.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartGas.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistInListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsPage" /> class.
        /// </summary>
        public RegistInListPage()
        {
            this.InitializeComponent();
            this.BindingContext = new RegistInListViewModel();
        }

        protected override async void OnAppearing()
        {
           //await (BindingContext as RegistInListViewModel).LoadData();
        }
    }
}