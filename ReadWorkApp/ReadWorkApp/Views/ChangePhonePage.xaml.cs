﻿using ReadWorkApp.ViewModel;
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
    public partial class ChangePhonePage : ContentPage
    {
        public ChangePhonePage()
        {
            InitializeComponent();

            BindingContext = new ChangePhoneViewModel();
        }
    }
}