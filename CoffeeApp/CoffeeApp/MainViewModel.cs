using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CoffeeApp
{
    public class MainViewModel : BaseViewModel
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public ICommand ChangeName { get; }

        public MainViewModel()
        {
            ChangeName = new Command(() =>
            {
                Name = "A"+ DateTime.Now;
            });
        }
    }
}
