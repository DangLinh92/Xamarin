using SmartGas.ApiService;
using SmartGas.Helpers;
using SmartGas.Models;
using SmartGas.Service;
using SmartGas.Services;
using SmartGas.Utilities;
using SmartGas.Validators;
using SmartGas.Validators.Rules;
using System;
using System.Collections.Generic;
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
    public class RegistInViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistInViewModel" /> class
        /// </summary>
        public RegistInViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            SubmitCommand = new Command(this.SubmitClicked);
            ScanQRCodeCommand = CommandFactory.Create(ScanQRCodeClicked);
            UnitTappedCommand = CommandFactory.Create(UnitTappedAction);
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
            get { return !GetActiveByDepartment.Active() && false; }
        }

        private float _qtyPerStock;
        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the Street Address from user.
        /// </summary>
        public float Qtyperstock
        {
            get => _qtyPerStock;
            set
            {
                SetProperty(ref _qtyPerStock, value);
            }
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
        public bool LocationVisible { get => false; }

        private DateTime _dateSx;
        public DateTime DateSX
        {
            get => _dateSx;
            set
            {
                SetProperty(ref _dateSx, value);
            }
        }
        public bool DateSXVisible { get => !GetActiveByDepartment.Active() && false; }

        private DateTime _dateEx;
        public DateTime DateEx
        {
            get => _dateEx;
            set
            {
                SetProperty(ref _dateEx, value);
            }
        }
        public bool DateExVisible { get => !GetActiveByDepartment.Active(); }

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

        public IAsyncCommand UnitTappedCommand { get; set; }

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

        private async Task UnitTappedAction()
        {
            if (GetActiveByDepartment.Active())
            {
                string dept = Application.Current.Resources["Department"] + "";
                List<Unit> lstUnit = await InOutStockService.GetUnitBySparepare(dept, Code.Value);
                if (Units.Count == 0)
                {
                    Units.AddRange(lstUnit);
                }
            }
        }

        private async Task GetUnits()
        {
            if (GetActiveByDepartment.Active())
            {
                string dept = Application.Current.Resources["Department"] + "";
                List<Unit> lstUnit = await InOutStockService.GetUnit(dept);

                if (Units.Count == 0)
                {
                    Units.AddRange(lstUnit);
                }
            }
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
            bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Xác nhận nhập kho", "Yes", "No");

            if (answer == false)
                return;

            if (this.AreFieldsValid())
            {
                PutInOutModel model = new PutInOutModel();
                if (GetActiveByDepartment.Active())
                {
                    model.spare_part_code = this.Code.Value;
                    model.name = this.Name;
                    model.quantity = this.Quantity.Value;
                    model.unit = this.Unit;
                    model.in_out = Constants.IN;
                    model.user_create = Preferences.Get("userId", "") + "-" + UserCreate;
                    model.department = Application.Current.Resources["Department"] + "";
                }
                else
                {
                    model.spare_part_code = this.Code.Value;
                    model.quantity = this.Quantity.Value;
                    model.quantity_per_stock = this.Qtyperstock;
                    model.in_out = Constants.IN;
                    model.exprired_date = DateEx.ToString("yyyyMMdd");
                    model.user_create = Preferences.Get("userId", "") + "-" + UserCreate;
                    model.department = Application.Current.Resources["Department"] + "";
                }

                if (await InOutStockService.InStock(model))
                {
                    await Application.Current.MainPage.DisplayAlert("Nhập kho thành công", "Import success", "OK");
                    ClearData();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Nhập kho thất bại", "Import fault", "OK");
                }
            }
        }

        private void ClearData()
        {
            Code.Value = "";
            Name = "";
            Quantity.Value = 0;
            Unit = "";
            LotNo = "";
            Location = "";
            DateEx = DateTime.Now;
            DateSX = DateTime.Now;
            UserCreate = "";
            Qtyperstock = 0;
            if (Application.Current.Resources.ContainsKey("QrCode"))
            {
                Application.Current.Resources["QrCode"] = "";
            }
        }

        private async Task GetDataFromQRCode(string qrCode)
        {
            if (string.IsNullOrEmpty(qrCode))
            {
                return;
            }

            if (GetActiveByDepartment.Active()) // spare part system  TEST1234$VI TRI$OK$2021-12-28$2199-01-01$1$EA
            {
                string[] infors = qrCode.Split('$');
                if (infors.Length >= 7)
                {
                    if (Application.Current.Resources["Department"] + "" == Constants.SMT_DEPT)
                    {
                        List<string> spCodes = await InOutStockService.GetSparepartByEncript(Constants.SMT_DEPT, infors[0]);

                        if (spCodes.Count > 0)
                        {
                            Code.Value = spCodes[0];
                        }
                        else
                        {
                            Code.Value = infors[0];
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
                    await NameFocusedAction();
                }
                else
                {
                    ClearData();
                }
            }
            else // mro system TEST_1234$QTY$QTY_PER_STOCK$EX_PRI DATE
            {
                string[] infors = qrCode.Split('$');
                if (infors.Length >= 4)
                {
                    Code.Value = infors[0];
                    Quantity.Value = float.Parse(infors[1]);
                    Qtyperstock = float.Parse(infors[2]);
                    DateEx = !DateTime.TryParse(infors[3],out _) ? DateTime.MaxValue : DateTime.Parse(infors[3]);
                    UserCreate = Preferences.Get("userId", "");
                }
            }
        }
        #endregion
    }
}