using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Common
{
    public static class Constants
    {
        public const string SMT_DEPT = "SMT";
        public const string CSP_DEPT = "CSP";
        public const string LFEM_DEPT = "LFEM";
        public const string WLP1_DEPT = "WLP1";
        public const string WLP2_DEPT = "WLP2";
        public const string UTILITY_DEPT = "UTILITY";

        public static string GetStockCode(string dept)
        {
            string result = "";
            switch (dept)
            {
                case SMT_DEPT:
                    result = "SMT_K1";
                    break;
                case CSP_DEPT:
                    result = "CSP_K1";
                    break;
                case LFEM_DEPT:
                    result = "LFEM_K1";
                    break;
                case WLP1_DEPT:
                    result = "WLP1_K1";
                    break;
                case WLP2_DEPT:
                    result = "WLP2_K1";
                    break;
                case UTILITY_DEPT:
                    result = "UTILITY_K1";
                    break;
            }
            return result;
        }
    }
}
