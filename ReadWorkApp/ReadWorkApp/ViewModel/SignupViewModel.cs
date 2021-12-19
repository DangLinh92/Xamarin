using ReadWorkApp.Services;
using ReadWorkApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReadWorkApp.ViewModel
{
    public class SignupViewModel : ObservableObject
    {
        public SignupViewModel()
        {
            SignUpCommand = CommandFactory.Create(SignUpAction);
            LoginCommand = CommandFactory.Create(LoginAction);
        }

        private async Task LoginAction()
        {
           var response =  await ApiService.Login(_email, _password);
            if (response)
            {
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("YourApp", "Signup fault", "Ok");
            }
        }

        private async Task SignUpAction()
        {
          var response = await ApiService.RegisterUser(_name, _email, _password);
            if (response)
            {
                //await App.Current.MainPage.DisplayAlert("YourApp", "Signup ok", "Ok");
                await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("YourApp", "Signup fault", "Ok");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public IAsyncCommand SignUpCommand { get; }
        public IAsyncCommand LoginCommand { get; }
    }
}
