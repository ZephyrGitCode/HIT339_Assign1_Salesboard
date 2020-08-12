using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Assign1_Salesboard_Zephyr.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Assign1_Salesboard_ZephyrUser class
    public class Zephyr_ApplicationUser : IdentityUser
    {
        public string CustomTag { get; set; }
    }
}
