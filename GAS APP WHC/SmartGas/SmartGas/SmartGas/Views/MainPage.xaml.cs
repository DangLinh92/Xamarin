using DevExpress.XamarinForms.Navigation;
using SmartGas.ViewModels;
using System;
using System.Linq;

namespace SmartGas.Views
{
    public partial class MainPage : TabPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
