using SmartGas.ApiService;
using SmartGas.Models;
using System;
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
    public class RegistOutListViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;

        private Command<object> menuCommand;

        #endregion
        public Action GetDataAction;
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DocumentsViewModel"/> class.
        /// </summary>
        public RegistOutListViewModel()
        {
            PutOutList = new ObservableCollection<PutOutModel>();
            GetDataAction = async () =>
            {
                await LoadData();
            };
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
        [DataMember(Name = "PutOutList")]
        public ObservableCollection<PutOutModel> PutOutList { get; set; }

        #endregion

        #region Methods

        public async Task LoadData()
        {
            try
            {
                string dept = Application.Current.Resources["Department"] + "";
                List<PutOutModel> lstPutIn = await InOutStockService.GetPutOutHistory(dept);
                PutOutList.Clear();
                foreach (var item in lstPutIn)
                {
                    PutOutList.Add(item);
                }
            }
            catch (System.Exception)
            {
                PutOutList = new ObservableCollection<PutOutModel>();
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