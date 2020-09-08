
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assign1_Salesboard_Zephyr.DBData;

namespace Assign1_Salesboard_Zephyr.Data
{
    public class Zephyr_ApplicationContext : IdentityDbContext<Zephyr_ApplicationUser>
    {
        public Zephyr_ApplicationContext(DbContextOptions<Zephyr_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<Cart> Cart { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Item>().Property(b => b.Postdate).HasDefaultValueSql("getdate()");
        }
    }
}
