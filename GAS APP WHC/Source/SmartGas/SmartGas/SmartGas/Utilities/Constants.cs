using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartGas.Utilities
{
    public class Constants
    {
        public const string API_URL = "http://10.70.10.97:8087/api/"; /*"http://10.70.10.97:8087/api/";*/
        public const string SMT_DEPT = "SMT";
        public const string CSP_DEPT = "CSP";
        public const string LFEM_DEPT = "LFEM";
        public const string WLP1_DEPT = "WLP1";
        public const string WLP2_DEPT = "WLP2";
        public const string UTILITY_DEPT = "UTILITY";
        public const string IN = "IN";
        public const string OUT = "OUT";

        public const string DatabaseFilename = "SmartGasSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
