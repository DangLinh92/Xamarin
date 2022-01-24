using SmartGas.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartGas.Views
{
    /// <summary>
    /// Page to list out article items.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryListPage" /> class.
        /// </summary>
        public InventoryListPage()
        {
            this.InitializeComponent();
            this.BindingContext = new InventoryListViewModel();
        }

        protected override void OnAppearing()
        {
            ((InventoryListViewModel)this.BindingContext)?.GetDataAction.Invoke();
        }
    }
}