using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Data
{
    public class SmartGasContext : DbContext
    {
        public SmartGasContext(DbContextOptions options) : base(options)
        {

        }

        public virtual object GetUser() {
            return null;
        }
    }
}
