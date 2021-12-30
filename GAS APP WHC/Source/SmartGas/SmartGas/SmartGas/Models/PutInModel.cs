using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGas.Models
{
    public class PutInModel: Xamarin.CommunityToolkit.ObjectModel.ObservableObject
    {
        private string _spareCode;
        public string SPARE_PART_CODE 
        {
            get => _spareCode;
            set => SetProperty(ref _spareCode, value);
        }

        public string _name;
        public string NAME 
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private double _qty;
        public double QUANTITY 
        {
            get => _qty;
            set => SetProperty(ref _qty, value);
        }

        private string _unit;
        public string UNIT 
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        private string _createDate;
        public string CREATE_DATE 
        {
            get => _createDate;
            set => SetProperty(ref _createDate, value);
        }
    }
}
