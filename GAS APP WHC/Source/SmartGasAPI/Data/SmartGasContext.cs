using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Data
{
    public class SmartGasContext : DbContext
    {
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SmartGasContext));

        public SmartGasContext(DbContextOptions options) : base(options)
        {

        }

        public virtual object GetUser() {
            return null;
        }
    }
}
