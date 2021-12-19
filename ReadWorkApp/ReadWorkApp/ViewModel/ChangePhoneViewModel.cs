using ReadWorkApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ReadWorkApp.ViewModel
{
    public class ChangePhoneViewModel : ObservableObject
    {
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        public IAsyncCommand ChangePhoneCmd { get; }

        public ChangePhoneViewModel()
        {
            ChangePhoneCmd = CommandFactory.Create(ChangePhone);
        }

        private async Task ChangePhone()
        {
            var result = await ApiService.EditPhoneNumber(phoneNumber);

            if (!result)
            {
                await App.Current.MainPage.DisplayAlert("error", "something wrong", "Cancel");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("success", "OK", "Alright");
            }
        }
    }
}
