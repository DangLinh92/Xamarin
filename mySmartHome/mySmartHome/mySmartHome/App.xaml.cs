using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mySmartHome
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            var simulator = new SmartHomeSimulator();
            simulator.Run();
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
