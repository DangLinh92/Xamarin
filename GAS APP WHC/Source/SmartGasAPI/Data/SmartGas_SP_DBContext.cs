using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartGasAPI.Models.SPAREPART;

namespace SmartGasAPI.Data
{
    
    public class SmartGas_SP_DBContext : DbContext
    {
        public SmartGas_SP_DBContext(DbContextOptions<SmartGas_SP_DBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(new string[] { "PLANT", "DEPARTMENT", "USER_ID" });
        }
    }
}
