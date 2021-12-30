using SmartGas.ApiService;
using SmartGas.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartGas.ViewModels
{
    /// <summary>
    /// ViewModel for documents page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class RegistInListViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;

        private Command<object> menuCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DocumentsViewModel"/> class.
        /// </summary>
        public RegistInListViewModel()
        {
            PutInList = new ObservableCollection<PutInModel>();
            _ = LoadData();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        /// <summary>
        /// Gets the command that will be executed when an menu button is selected.
        /// </summary>
        public Command<object> MenuCommand
        {
            get
            {
                return this.menuCommand ?? (this.menuCommand = new Command<object>(this.MenuButtonClicked));
            }
        }

        /// <summary>
        /// Gets or sets a collction of value to be displayed in documents list page.
        /// </summary>
        [DataMember(Name = "PutInList")]
        public ObservableCollection<PutInModel> PutInList { get; set; }

        #endregion

        #region Methods

        public async Task LoadData()
        {
            try
            {
                string dept = Application.Current.Resources["Department"] + "";
                List<PutInModel> lstPutIn = await InOutStockService.GetPutInHistory(dept);
                //PutInList = new ObservableCollection<PutInModel>(lstPutIn);
                foreach (var item in lstPutIn)
                {
                    PutInList.Add(item);
                }
            }
            catch (System.Exception)
            {
                PutInList = new ObservableCollection<PutInModel>();
            }
        }

        /// <summary>
        /// Invoked when an item is selected from the documents list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the more button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void MenuButtonClicked(object obj)
        {
            // Do something
        }
        #endregion
    }
}