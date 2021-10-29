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
    public partial class StackLayout : ContentPage
    {
        public StackLayout()
        {
            InitializeComponent();
        }

        private void OnNormalTip(object sender, EventArgs e)
        {

        }

        private void OnGenerousTip(object sender, EventArgs e)
        {

        }

        private void roundDown_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        private void roundUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new OldMainPage());
        }

    }
}