using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public const string TEXT = "DANGLV1";

        private void translateButton_Clicked(object sender, EventArgs e)
        {

        }

        private void callButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}