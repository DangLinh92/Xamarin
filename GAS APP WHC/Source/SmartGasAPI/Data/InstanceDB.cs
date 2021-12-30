using Microsoft.EntityFrameworkCore;
using SmartGasAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Data
{
    public static class InstanceDB
    {
        public static SmartGasContext context(string department, SmartGas_SP_DBContext sparepartDB, SmartGas_MRO_DBcontext mroDB)
        {
            SmartGasContext db;
            switch (department)
            {
                case Constants.SMT_DEPT:
                case Constants.UTILITY_DEPT:
                case Constants.WLP2_DEPT:
                    db = sparepartDB;
                    break;
                default:
                    db = mroDB;
                    break;
            }
            return db;
        }
    }
}
