using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assign1_Salesboard_Zephyr.Data
{
    public class Assign1_Salesboard_ZephyrContext : IdentityDbContext<Assign1_Salesboard_ZephyrUser>
    {
        public Assign1_Salesboard_ZephyrContext(DbContextOptions<Assign1_Salesboard_ZephyrContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
