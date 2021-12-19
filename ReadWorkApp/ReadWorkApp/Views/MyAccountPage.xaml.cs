using ReadWorkApp.Services;
using ReadWorkApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadWorkApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        public MyAccountPage()
        {
            InitializeComponent();
            BindingContext = new MyAccountPageViewModel() {
                ImgProfile = this.ImgProfile
            };
        }

        protected override async void OnAppearing()
        {
           var profileImage = await ApiService.UserProfileImage();
            if (string.IsNullOrEmpty(profileImage.imageUrl))
            {
                ImgProfile.Source = "userPlaceholder.png";
            }
            else
            {
                ImgProfile.Source = profileImage.FullImagePath;
            }
        }
    }
}