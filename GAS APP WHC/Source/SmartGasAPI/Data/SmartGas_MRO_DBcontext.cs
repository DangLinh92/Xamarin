using Microsoft.EntityFrameworkCore;
using SmartGasAPI.Models.MRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGasAPI.Data
{

    public class SmartGas_MRO_DBcontext : DbContext
    {
        public SmartGas_MRO_DBcontext(DbContextOptions<SmartGas_MRO_DBcontext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(new string[] { "PLANT", "DEPARTMENT", "USER_ID" });
        }
    }
}
