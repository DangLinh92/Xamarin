using SmartGas.ApiService;
using SmartGas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Model = SmartGas.Models.Story;

namespace SmartGas.ViewModels
{
    /// <summary>
    /// ViewModel for article list page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    [DataContract]
    public class InventoryListViewModel : BaseViewModel
    {
        #region Fields

        private Command menuCommand;

        private Command bookmarkCommand;

        private Command featureStoriesCommand;

        private Command itemSelectedCommand;

        private ObservableCollection<InventoryGasModel> gasInventories;

        public Action GetDataAction;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="InventoryListViewModel" /> class.
        /// </summary>
        public InventoryListViewModel()
        {
            gasInventories = new ObservableCollection<InventoryGasModel>();
            GetDataAction = async () =>
            {
                await GetData();
            };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the property that has been bound with the list view, which displays the articles latest stories items.
        /// </summary>
        [DataMember(Name = "GasInventories")]
        public ObservableCollection<InventoryGasModel> GasInventories
        {
            get
            {
                return this.gasInventories;
            }

            set
            {
                if (this.gasInventories == value)
                {
                    return;
                }

                this.SetProperty(ref this.gasInventories, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand
        {
            get
            {
                return this.menuCommand ?? (this.menuCommand = new Command(this.MenuClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the bookmark button is clicked.
        /// </summary>
        public Command BookmarkCommand
        {
            get
            {
                return this.bookmarkCommand ?? (this.bookmarkCommand = new Command(this.BookmarkButtonClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will executed when the feature stories item is clicked.
        /// </summary>
        public Command FeatureStoriesCommand
        {
            get
            {
                return this.featureStoriesCommand ?? (this.featureStoriesCommand = new Command(this.FeatureStoriesClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get
            {
                return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected));
            }
        }

        #endregion

        #region Methods


        private async Task GetData()
        {
            try
            {
                string dept = Application.Current.Resources["Department"] + "";
                List<InventoryGasModel> lstGas = await InOutStockService.GetGasInventory(dept);
                GasInventories.Clear();
                foreach (var item in lstGas)
                {
                    GasInventories.Add(item);
                }
            }
            catch (System.Exception)
            {
                gasInventories = new ObservableCollection<InventoryGasModel>();
            }
        }

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "SmartGas.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void MenuClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the bookmark button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void BookmarkButtonClicked(object obj)
        {
            if (obj is Model article)
            {
                article.IsBookmarked = !article.IsBookmarked;
            }
        }

        /// <summary>
        /// Invoked when the the feature stories item is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void FeatureStoriesClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ItemSelected(object obj)
        {
            // Do something
        }

        #endregion
    }
}
