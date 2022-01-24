using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGas.Models
{
    public class ReturnGasModel : Xamarin.CommunityToolkit.ObjectModel.ObservableObject
    {
        private string _gasId;
        public string GAS_ID
        {
            get => _gasId;
            set => SetProperty(ref _gasId, value);
        }

        private string _dateReturn;
        public string DATE_RETURN
        {
            get => _dateReturn;
            set => SetProperty(ref _dateReturn, value);
        }

        private string _userReturn;
        public string USER_RETURN
        {
            get => _userReturn;
            set => SetProperty(ref _userReturn, value);
        }

        private string _deptCode;
        public string DEPT_CODE
        {
            get => _deptCode;
            set => SetProperty(ref _deptCode, value);
        }
    }
}
