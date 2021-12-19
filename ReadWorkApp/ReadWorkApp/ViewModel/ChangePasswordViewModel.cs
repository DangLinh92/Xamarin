using ReadWorkApp.Services;
using ReadWorkApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ReadWorkApp.ViewModel
{
    public class ChangePasswordViewModel : ObservableObject
    {
        private string oldPassword;
        private string newPassword;
        private string confirmPassword;

        public string OldPassword 
        {
            get => oldPassword;
            set
            {
                SetProperty(ref oldPassword, value);
            }
        }

        public string NewPassword
        {
            get => newPassword;
            set
            {
                SetProperty(ref newPassword, value);
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                SetProperty(ref confirmPassword, value);
            }
        }

        public IAsyncCommand ChangePasswordCmd { get; }
        public ChangePasswordViewModel()
        {
            ChangePasswordCmd = CommandFactory.Create(ChangePassword);
        }

        private async Task ChangePassword()
        {
          var result = await ApiService.ChangePassword(oldPassword, newPassword, confirmPassword);
            if (!result)
            {
                await App.Current.MainPage.DisplayAlert("error", "something wrong", "Cancel");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Password changed", "OKE", "Alright");
                Preferences.Set("accessToken", "");
                Application.Current.MainPage = new NavigationPage(new SignupPage());
            }
        }
    }
}
