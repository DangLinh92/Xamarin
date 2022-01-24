using SmartGas.ApiService;
using SmartGas.DataService;
using SmartGas.Helpers;
using SmartGas.Models;
using SmartGas.Service;
using SmartGas.Utilities;
using SmartGas.Validators;
using SmartGas.Validators.Rules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartGas.ViewModels
{
    /// <summary>
    /// ViewModel for Business Registration Form page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class RegistOutViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistOutViewModel" /> class
        /// </summary>
        public RegistOutViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            SubmitCommand = new Command(this.SubmitClicked);
            ScanQRCodeCommand = CommandFactory.Create(ScanQRCodeClicked);
            CodeChangedCommand = new Command(CodeChangedAction);
            NameFocusedCommand = CommandFactory.Create(NameFocusedAction);
        }

        private async Task ScanQRCodeClicked()
        {
            await Navigation.NavigateToAsync<ScanQRViewModel>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the Full Name from user.
        /// </summary>
        public ValidatableObject<string> Code { get; set; }

        private string _name;
        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the Business Name from user.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public ValidatableObject<float> Quantity { get; set; }

        private string _unit;
        /// <summary>
        /// Gets or sets the property that bounds with a ComboBox that gets the Business from user.
        /// </summary>
        public string Unit
        {
            get => _unit;
            set
            {
                SetProperty(ref _unit, value);
            }
        }
        public bool UnitVisible
        {
            get => GetActiveByDepartment.Active();
        }

        private string _lotNo;
        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the Phone Number from user.
        /// </summary>
        public string LotNo
        {
            get => _lotNo;
            set
            {
                SetProperty(ref _lotNo, value);
            }
        }

        public bool LotNoVisible
        {
            get { return !GetActiveByDepartment.Active(); }
        }

        public bool QtyperstockVisible { get => !GetActiveByDepartment.Active(); }

        private string _userCreate;
        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the City from user.
        /// </summary>
        public string UserCreate
        {
            get => _userCreate;
            set
            {
                SetProperty(ref _userCreate, value);
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                SetProperty(ref _location, value);
            }
        }
        public bool LocationVisible { get => GetActiveByDepartment.Active(); }

        private DateTime _dateIn;
        public DateTime DateIn
        {
            get => _dateIn;
            set
            {
                SetProperty(ref _dateIn, value);
            }
        }
        public bool DateInVisible { get => GetActiveByDepartment.Active(); }

        private DateTime _dateEx;
        public DateTime DateEx
        {
            get => _dateEx;
            set
            {
                SetProperty(ref _dateEx, value);
            }
        }
        public bool DateExVisible { get => GetActiveByDepartment.Active(); }

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

        #region Comments

        /// <summary>
        /// Gets or sets the command is executed when the Submit button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        public IAsyncCommand ScanQRCodeCommand { get; set; }

        public IAsyncCommand LocationtappedCommand { get; set; }

        public Command CodeChangedCommand { get; set; }

        public IAsyncCommand NameFocusedCommand { get; set; }

        #endregion

        #region Methods
        private async Task NameFocusedAction()
        {
            if (GetActiveByDepartment.Active())
            {
                string dept = Application.Current.Resources["Department"] + "";
                List<PartInfo> lstPart = await InOutStockService.GetNameBySparepare(dept, Code.Value);

                Name = lstPart.Count <= 0 ? "" : lstPart[0].NAME;
            }
        }

        private void CodeChangedAction()
        {
            Units.Clear();
        }

        /// <summary>
        /// Initializzing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Code = new ValidatableObject<string>();
            this.Quantity = new ValidatableObject<float>();
        }

        /// <summary>
        /// Validation rule for name
        /// </summary>
        private void AddValidationRules()
        {
            this.Code.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Code Required" });
            this.Quantity.Validations.Add(new IsNotNullOrEmptyRule<float> { ValidationMessage = "Quantity Required" });
        }

        /// <summary>
        /// Check name is valid or not
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        private bool AreFieldsValid()
        {
            bool isCodeValid = this.Code.Validate();
            bool isqtyValid = this.Quantity.Validate();
            return isCodeValid && isqtyValid;
        }

        /// <summary>
        /// Invoked when the Submit button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void SubmitClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Xác nhận xuất kho", "Yes", "No");
                if (answer == false)
                    return;

                //if (await ProcessNoInternet())
                //{
                PutInOutModel model = new PutInOutModel();
                if (GetActiveByDepartment.Active())// SPARE PART CODE
                {
                    model.spare_part_code = this.Code.Value;
                    model.name = this.Name;
                    model.quantity = this.Quantity.Value;
                    model.unit = this.Unit;
                    model.in_out = Constants.OUT;
                    model.location = (Location + "").Trim();
                    model.create_date = DateIn.ToString("yyyy-MM-dd");
                    model.exprired_date = DateEx.ToString("yyyy-MM-dd");
                    model.user_create = UserCreate;
                    model.department = Application.Current.Resources["Department"] + "";
                }
                else
                {
                    model.spare_part_code = this.Code.Value;
                    model.quantity = this.Quantity.Value;
                    model.in_out = Constants.OUT;
                    model.create_date = DateIn.ToString("yyyy-MM-dd");
                    model.lot_no = LotNo;
                    model.tran_user = Preferences.Get("userId", "");
                    model.nguoi_thao_tac = UserCreate;
                    model.department = Application.Current.Resources["Department"] + "";
                }

                if (await InOutStockService.InStock(model))
                {
                    await Application.Current.MainPage.DisplayAlert("Xuất kho thành công", "Export success", "OK");
                    ClearData();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Xuất kho thất bại", "Export fault", "OK");
                }
                //}
            }
        }

        public async Task<bool> ProcessNoInternet()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                var profiles = Connectivity.ConnectionProfiles;

                if (current == NetworkAccess.Internet && profiles.Contains(ConnectionProfile.WiFi))
                {
                    return true;
                }
                else
                {
                    OUT_PUT_INFO model = new OUT_PUT_INFO();
                    if (GetActiveByDepartment.Active())// SPARE PART CODE
                    {
                        model.spare_part_code = this.Code.Value;
                        model.name = this.Name == "" ? this.Code.Value : Name;
                        model.quantity = this.Quantity.Value + "";
                        model.unit = this.Unit;
                        model.in_out = Constants.OUT;
                        model.location = (Location + "").Trim();
                        model.create_date = DateIn.ToString("yyyy-MM-dd");
                        model.exprired_date = DateEx.ToString("yyyy-MM-dd");
                        model.user_create = UserCreate;
                        model.department = Application.Current.Resources["Department"] + "";
                    }
                    else
                    {
                        model.spare_part_code = this.Code.Value;
                        model.quantity = this.Quantity.Value + "";
                        model.in_out = Constants.OUT;
                        model.create_date = DateIn.ToString("yyyy-MM-dd");
                        model.lot_no = LotNo;
                        model.tran_user = Preferences.Get("userId", "");
                        model.nguoi_thao_tac = UserCreate;
                        model.department = Application.Current.Resources["Department"] + "";
                    }
                    model.isDelete = "N";

                    SmartGasDatabase localDB = await SmartGasDatabase.Instance;
                    int row = await localDB.SaveItemAsync(model);
                    if (row > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Xuất kho tạm thành công", "Export success", "OK");
                        ClearData();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Xuất kho thất bại", "Export fault", "OK");
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Xuất kho thất bại", "Export fault", "OK");
                return false;
            }
        }

        public void ClearData()
        {
            Code.Value = "";
            Name = "";
            Quantity.Value = 0;
            Unit = "";
            LotNo = "";
            Location = "";
            DateEx = DateTime.Now;
            DateIn = DateTime.Now;
            UserCreate = "";
            if (Application.Current.Resources.ContainsKey("QrCode"))
            {
                Application.Current.Resources["QrCode"] = "";
            }
        }

        public async Task GetDataFromQRCode(string qrCode)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                var profiles = Connectivity.ConnectionProfiles;

                //if (current == NetworkAccess.Internet && profiles.Contains(ConnectionProfile.WiFi))
                //{
                if (string.IsNullOrEmpty(qrCode))
                {
                    return;
                }

                string deptCode = Application.Current.Resources["Department"] + "";
                if (GetActiveByDepartment.Active()) // spare part system  TEST1234$VI TRI$OK$2021-12-28$2199-01-01$1$EA ---  TEST1234$$OK$2021-12-28$2199-01-01$1$EA
                {
                    if (!qrCode.Contains("$"))
                    {
                        qrCode = await InOutStockService.GetGasInforInSparepart(deptCode, qrCode);// QR CODE : ARGON
                    }

                    string[] infors = qrCode.Split('$');

                    if (infors.Length >= 7)
                    {
                        if (infors[0].Length > 8 && Application.Current.Resources["Department"] + "" == Constants.SMT_DEPT)
                        {
                            List<string> spCodes = await InOutStockService.GetSparepartByEncript(Constants.SMT_DEPT, infors[0]);

                            if (spCodes.Count > 0)
                            {
                                Code.Value = spCodes[0];
                            }
                        }
                        else
                        {
                            Code.Value = infors[0];
                        }

                        Quantity.Value = float.Parse(infors[5]);
                        Unit = infors[6];
                        Location = infors[1];
                        UserCreate = Preferences.Get("userId", "");
                        DateIn = DateTime.Parse(infors[3]);
                        DateEx = DateTime.Parse(infors[4]);
                        await NameFocusedAction();
                    }
                    else
                    {
                        ClearData();
                    }
                }
                else // mro system TEST_1234$QTY$QTY_PER_STOCK$EX_PRI DATE$DATE_IN$LOT_NO   TEST_1234$1$1$0011$2021-12-30$211230TEST_12340001$WLP1
                {
                    if (!qrCode.Contains("$"))
                    {
                        qrCode = await InOutStockService.GetGasInforInSparepart(deptCode, qrCode);// QR CODE : ARGON
                    }

                    string[] infors = qrCode.Split('$');

                    if (infors.Length >= 6)
                    {
                        Code.Value = infors[0];
                        Quantity.Value = float.Parse(infors[1]);
                        DateEx = !DateTime.TryParse(infors[3], out _) ? DateTime.MaxValue : DateTime.Parse(infors[3]);
                        UserCreate = Preferences.Get("userId", "");
                        LotNo = infors[5];
                        DateIn = DateTime.ParseExact(infors[4], "yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                    }
                }
                //}
                //else
                //{
                //    if (string.IsNullOrEmpty(qrCode))
                //    {
                //        return;
                //    }

                //    SmartGasDatabase localDB = await SmartGasDatabase.Instance;
                //    OUT_PUT_INFO model = new OUT_PUT_INFO();
                //    model.isDelete = "N";
                //    model.create_date = DateTime.Now.ToString("yyyy-MM-dd");
                //    model.tran_user = Preferences.Get("userId", "");
                //    model.nguoi_thao_tac = UserCreate;
                //    model.department = Application.Current.Resources["Department"] + "";
                //    model.qrCode = qrCode;

                //    int row = await localDB.SaveItemAsync(model);
                //    if (row > 0)
                //    {
                //        await Application.Current.MainPage.DisplayAlert("Xuất kho tạm thành công", "Export success", "OK");
                //    }
                //    else
                //    {
                //        await Application.Current.MainPage.DisplayAlert("Xuất kho thất bại", "Export fault", "OK");
                //    }
                //}
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Scan is error", ex.Message, "OK");
            }
            finally
            {
                if (Application.Current.Resources.ContainsKey("QrCode"))
                {
                    Application.Current.Resources["QrCode"] = "";
                }
            }
        }
        #endregion
    }
}