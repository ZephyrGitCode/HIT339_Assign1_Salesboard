using System;
using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
using Assign1_Salesboard_Zephyr.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Assign1_Salesboard_Zephyr.Areas.Identity.IdentityHostingStartup))]
namespace Assign1_Salesboard_Zephyr.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Assign1_Salesboard_ZephyrContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));
                /*
                services.AddDefaultIdentity<Assign1_Salesboard_ZephyrUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Assign1_Salesboard_ZephyrContext>();
                */

                services.AddIdentityCore<Assign1_Salesboard_ZephyrUser>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI();
            });
        }
    }
}