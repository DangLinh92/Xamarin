using FFImageLoading.Forms;
using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public class MyAccountPageViewModel : ObservableObject
    {
        public IAsyncCommand TapUploadImageCmd { get; }
        public IAsyncCommand ChangePassword { get; }
        public IAsyncCommand ChangePhone { get; }

        public CachedImage ImgProfile { get; set; }

        private MediaFile file;
        public MyAccountPageViewModel()
        {
            TapUploadImageCmd = CommandFactory.Create(TapUploadImage);
            ChangePassword = CommandFactory.Create(ChangemyPassword);
            ChangePhone = CommandFactory.Create(ChangemyPhone);
        }

        private async Task ChangemyPhone()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ChangePhonePage());
        }

        private async Task ChangemyPassword()
        {
           await App.Current.MainPage.Navigation.PushAsync(new ChangePasswordPage());
        }

        private async Task TapUploadImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
               await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "Sample",
            //    Name = "test.jpg"
            //});

            file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                AddImageToServer();
                return stream;
            });
        }

        private async void AddImageToServer()
        {
            var imageArray = FromFile.ToArray(file.GetStream());
            file.Dispose();
           var response = await ApiService.EditUserProfile(imageArray);
            if (response)
            {
                return;
            }
           await DisplayAlert("Wrong", "Please upload the image again","Alright");
        }

       
        private async Task DisplayAlert(string v1, string v2, string v3)
        {
            await App.Current.MainPage.DisplayAlert(v1, v2, v3);
        }
    }
}
