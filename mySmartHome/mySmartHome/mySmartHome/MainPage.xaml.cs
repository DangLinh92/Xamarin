using mySmartHome.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mySmartHome
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = DeviceManager.Instance.Value.Devices
                .OrderBy(dev => dev.Name)
                .ToLookup(dev => dev.Name[0].ToString());
        }
    }
}
