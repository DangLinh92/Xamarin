using ReadWorkApp.Services;
using ReadWorkApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReadWorkApp.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public IAsyncCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = CommandFactory.Create(LoginAction);
        }

        private async Task LoginAction()
        {
           bool result = await ApiService.Login(email, password);

            if (result)
            {
                // await App.Current.MainPage.DisplayAlert("Login success", "Login ok", "OK");
                //await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Login fault", "Login fault", "OK");
            }
        }
    }
}
