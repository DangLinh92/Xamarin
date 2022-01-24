using SmartGas.Models;
using SmartGas.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartGas.ViewModels
{
    /// <summary>
    /// ViewModel for stock overview page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class HomeViewModel : BaseViewModel
    {
        #region Field
        private ObservableCollection<CategoryAction> categoryActions;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="HomeViewModel" /> class.
        /// </summary>
        public HomeViewModel()
        {
            #region comment
            //string dept = Application.Current.Resources["Department"] + "";
            //if (dept == Constants.SMT_DEPT)
            //{
            //    categoryActions = new ObservableCollection<CategoryAction>()
            //        {
            //                new CategoryAction()
            //                {
            //                    CategoryName = "NHÂP KHO",
            //                    BackgroundGradientStart = "#f59083",
            //                    BackgroundGradientEnd = "#fae188",
            //                    Glyph = "\xe802"
            //                },
            //                 new CategoryAction()
            //                {
            //                    CategoryName = "XUẤT KHO",
            //                    BackgroundGradientStart = "#ff7272",
            //                    BackgroundGradientEnd = "#f650c5",
            //                    Glyph = "\xe803"
            //                },
            //                  new CategoryAction()
            //                {
            //                    CategoryName = "TRẢ BÌNH RỖNG",
            //                    BackgroundGradientStart = "#5e7cea",
            //                    BackgroundGradientEnd = "#1dcce3",
            //                    Glyph = "\xe805"
            //                }
            //                //},
            //                //    new CategoryAction()
            //                //{
            //                //    CategoryName = "BIỂU ĐỒ",
            //                //    BackgroundGradientStart = "#255ea6",
            //                //    BackgroundGradientEnd = "#b350d1",
            //                //    Glyph = "\xe800"
            //                //}
            //        };
            //}
            //else
            //{

            //}
            #endregion

            categoryActions = new ObservableCollection<CategoryAction>()
            {
                 new CategoryAction()
                 {
                    CategoryName = "XUẤT KHO",
                    BackgroundGradientStart = "#ff7272",
                    BackgroundGradientEnd = "#f650c5",
                    Glyph = "\xe803"
                 },
                 new CategoryAction()
                 {
                    CategoryName = "TRẢ BÌNH RỖNG",
                    BackgroundGradientStart = "#5e7cea",
                    BackgroundGradientEnd = "#1dcce3",
                    Glyph = "\xe805"
                 }
            };

            this.ProfileImage = App.ImageServerPath + "ProfileImage1.png";
            this.MenuCommand = new Command(this.MenuButtonClicked);
            this.ProfileSelectedCommand = new Command(this.ProfileImageClicked);
            this.RegistInCommand = new Command(RegisInClicked);
            this.RegistOutCommand = new Command(RegisOutClicked);
            this.RegistReturnCommand = new Command(RegisReturnClicked);
            this.RegistChartCommand = new Command(RegisChartClicked);

            UserId = Preferences.Get("userId", "");
            UserName = Preferences.Get("userName", "");
            UserInfo = UserName;
            RegistCommand = CommandFactory.Create<object>(RegisClickedAsync);
        }

        private async Task RegisClickedAsync(object obj)
        {
            var btn = obj as Syncfusion.XForms.Buttons.SfChip;
            if (btn.Text == "NHÂP KHO")
            {
                await Navigation.NavigateToAsync<RegistInMainActionViewModel>();
            }
            else if (btn.Text == "XUẤT KHO")
            {
                await Navigation.NavigateToAsync<RegistOutMainActionViewModel>();
            }
            else if (btn.Text == "TRẢ BÌNH RỖNG")
            {
                await Navigation.NavigateToAsync<RegistReturnGasViewModel>();
            }
            else if (btn.Text == "BIỂU ĐỒ")
            {

            }
        }

        private void RegisChartClicked(object obj)
        {
        }

        private void RegisReturnClicked(object obj)
        {
        }

        private void RegisOutClicked(object obj)
        {
            //await Navigation.NavigateToAsync<RegistOutViewModel>();
        }

        private void RegisInClicked(object obj)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        public string ProfileImage { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserInfo { get; set; }

        /// <summary>
        /// Gets the health care items collection.
        /// </summary>
        public ObservableCollection<CategoryAction> CategoryActions
        {
            get
            {
                return this.categoryActions;
            }

            private set
            {
                this.SetProperty(ref this.categoryActions, value);
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// Gets or sets the command is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }
        public IAsyncCommand<object> RegistCommand { get; set; }
        public Command RegistInCommand { get; set; }
        public Command RegistOutCommand { get; set; }
        public Command RegistReturnCommand { get; set; }
        public Command RegistChartCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the profile image is clicked.
        /// </summary>
        public Command ProfileSelectedCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the menu button is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void MenuButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the profile image is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProfileImageClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}
