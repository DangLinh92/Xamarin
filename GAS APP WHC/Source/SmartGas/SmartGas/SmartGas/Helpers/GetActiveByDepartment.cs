using SmartGas.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartGas.Helpers
{
    public static class GetActiveByDepartment
    {
        public static bool Active()
        {
            bool result = false;
            if (Application.Current.Resources.ContainsKey("Department"))
            {
                var department = Application.Current.Resources["Department"];
                switch (department)
                {
                    case Constants.SMT_DEPT:
                        result = true;
                        break;
                    default:
                        result = false;
                        break;
                }
            }
            return result;
        }
    }
}
