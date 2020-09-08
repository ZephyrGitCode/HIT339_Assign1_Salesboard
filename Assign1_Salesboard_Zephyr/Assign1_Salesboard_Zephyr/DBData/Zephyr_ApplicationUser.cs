using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assign1_Salesboard_Zephyr.DBData;
using Microsoft.AspNetCore.Identity;

namespace Assign1_Salesboard_Zephyr.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Assign1_Salesboard_ZephyrUser class
    public class Zephyr_ApplicationUser : IdentityUser
    {
        [StringLength(25), PersonalData]
        public string Fname { get; set; }

        [StringLength(50), PersonalData]
        public string Lname { get; set; }

        [Range(16,120), PersonalData]
        public int Age { get; set; }

        [PersonalData]
        public string Image { get; set; }

        [PersonalData]
        public string State { get; set; }

        [PersonalData]
        public string City { get; set; }

        [PersonalData]
        public string Postcode { get; set; }

        [PersonalData]
        public string Street { get; set; }

        [PersonalData]
        public bool IsAdmin { get; set; }
    }
}