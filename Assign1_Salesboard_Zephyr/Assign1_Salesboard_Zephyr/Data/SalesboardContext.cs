using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assign1_Salesboard_Zephyr.DBData;

namespace Assign1_Salesboard_Zephyr.Data
{
    public class SalesboardContext : DbContext
    {
        public SalesboardContext(DbContextOptions<SalesboardContext> options)
            : base(options)
        {
        }
        public DbSet<Items> Item { get; set; }
    }
}