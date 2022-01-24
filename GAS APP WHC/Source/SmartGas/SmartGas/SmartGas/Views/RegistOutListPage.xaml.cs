using SmartGas.DataService;
using SmartGas.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartGas.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistOutListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsPage" /> class.
        /// </summary>
        public RegistOutListPage()
        {
            this.InitializeComponent();
            this.BindingContext = new RegistOutListViewModel();
        }

        protected override void OnAppearing()
        {
            (BindingContext as RegistOutListViewModel)?.GetDataAction?.Invoke();
        }
    }
}