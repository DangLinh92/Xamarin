using SmartGas.ApiService;
using SmartGas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartGas.ViewModels
{
    /// <summary>
    /// ViewModel for documents page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class RegistReturnGasViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;

        private Command<object> menuCommand;

        public IAsyncCommand ScanQRCodeCommand { get; set; }

        private string _qrCode;
        public string QR_Code
        {
            get => _qrCode;
            set
            {
                SetProperty(ref _qrCode, value);
                _ = GetDataFromQRCode(value);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DocumentsViewModel"/> class.
        /// </summary>
        public RegistReturnGasViewModel()
        {
            ReturnGasList = new ObservableCollection<ReturnGasModel>();
            ScanQRCodeCommand = CommandFactory.Create(ScanQRCodeClicked);
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
        [DataMember(Name = "ReturnGasList")]
        public ObservableCollection<ReturnGasModel> ReturnGasList { get; set; }

        #endregion

        #region Methods
        private async Task ScanQRCodeClicked()
        {
            await Navigation.NavigateToAsync<ScanQRViewModel>();
        }

        public async Task LoadData()
        {
            try
            {
                string dept = Application.Current.Resources["Department"] + "";
                List<ReturnGasModel> lstPutIn = await InOutStockService.GetGasInforReturn(dept);
                ReturnGasList?.Clear();
                foreach (var item in lstPutIn)
                {
                    ReturnGasList.Add(item);
                }
            }
            catch (System.Exception)
            {
                ReturnGasList = new ObservableCollection<ReturnGasModel>();
            }
        }

        private async Task GetDataFromQRCode(string qrCode)
        {
            try
            {
                if (string.IsNullOrEmpty(qrCode))
                {
                    return;
                }
                bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Xác nhận trả kho bình khí :" + qrCode, "Yes", "No");
                if (answer == false)
                    return;

                string deptCode = Application.Current.Resources["Department"] + "";

                var result = await InOutStockService.PutGasInforReturn(new ReturnGasModel()
                {
                    GAS_ID = qrCode,
                    DEPT_CODE = deptCode,
                    USER_RETURN = Preferences.Get("userId", "")
                });

                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert("Infomation", "Success", "OK");
                    if (Application.Current.Resources.ContainsKey("QrCode"))
                    {
                        Application.Current.Resources["QrCode"] = "";
                    }
                    QR_Code = "";
                    await LoadData();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Infomation", "Error", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Scan is error", ex.Message, "OK");
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