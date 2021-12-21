using SmartGas.Services;
using SmartGas.ViewModels;
using SmartGas.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
     [assembly: ExportFont("UIFontGas.ttf", Alias = "FontIcons_GAS")]
namespace SmartGas
{
    public partial class App : Application
    {
        public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTUwNTM1QDMxMzkyZTM0MmUzMEs3UXpMcHk3VzdqeEVCRENxZkszM3R5Tk11TlJ4THQrQXJGTmY0akR3dkU9");

            DependencyService.Register<NavigationService>();

            InitializeComponent();

            var navigationService = DependencyService.Get<INavigationService>();
            // Use the NavigateToAsync<ViewModelName> method
            // to display the corresponding view.
            // Code lines below show how to navigate to a specific page.
            // Comment out all but one of these lines
            // to open the corresponding page.
            //navigationService.NavigateToAsync<LoginViewModel>();
            navigationService.NavigateToAsync<LoginViewModel>();

            // MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
