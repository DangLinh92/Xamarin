using SmartGas.ApiService;
using SmartGas.Validators;
using SmartGas.Validators.Rules;
using SmartGas.Views;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartGas.ViewModels
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginViewModel : LoginBaseViewModel
    {
        #region Fields

        private ValidatableObject<string> password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginViewModel" /> class.
        /// </summary>
        public LoginViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = CommandFactory.Create(LoginClicked);

            IsBusy = false;
            //this.SignUpCommand = new Command(this.SignUpClicked);
            //this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            //this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public ValidatableObject<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public IAsyncCommand LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Check the password is null or empty
        /// </summary>
        /// <returns>Returns the fields are valid or not</returns>
        public bool AreFieldsValid()
        {
            //bool isEmailValid = this.Email.Validate();
            bool isUserIdValid = UserId.Validate();
            bool isDepartmentValid = Department.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isUserIdValid && isPasswordValid && isDepartmentValid;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.UserId = new ValidatableObject<string>();
            this.Department = new ValidatableObject<string>();
            this.Password = new ValidatableObject<string>();
        }

        /// <summary>
        /// Validation rule for password
        /// </summary>
        private void AddValidationRules()
        {
            this.UserId.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "UserId Required" });
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password Required" });
            this.Department.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Department Required" });
        }

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async Task LoginClicked()
        {
            if (AreFieldsValid())
            {
                IsBusy = true;
                bool result = await LoginApiService.Login(UserId.Value, Password.Value, Department.Value);

                if (result)
                {
                    Application.Current.Resources["Department"] = Department.Value;
                    Application.Current.MainPage = new NavigationPage(new MainActionPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Login fault", "Login fault", "OK");
                }
                IsBusy = false;
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            // Do Something
            await Navigation.NavigateToAsync<SignUpViewModel>();
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}