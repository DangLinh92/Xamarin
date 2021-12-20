using SmartGas.ViewModels;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Xamarin.Forms.Internals;

namespace SmartGas.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MyOrdersDataService
    {
        #region fields

        private static MyOrdersDataService myOrdersDataService;

        private HistoryTransactionPageViewModel myOrderPageViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="MyOrdersDataService"/> class.
        /// </summary>
        private MyOrdersDataService()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of the <see cref="MyOrdersDataService"/>.
        /// </summary>
        public static MyOrdersDataService Instance => myOrdersDataService ?? (myOrdersDataService = new MyOrdersDataService());

        /// <summary>
        /// Gets or sets the value of my orders page view model.
        /// </summary>
        public HistoryTransactionPageViewModel HistoryTransactionPageViewModel =>
            this.myOrderPageViewModel = PopulateData<HistoryTransactionPageViewModel>("ecommerce.json");

        #endregion

        #region Methods

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

        #endregion
    }
}