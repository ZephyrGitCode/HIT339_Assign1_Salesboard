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
        [StringLength(25)]
        public string Fname { get; set; }

        [StringLength(50)]
        public string Lname { get; set; }

        [Range(16,120)]
        public int Age { get; set; }

        public string Image { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public string Street { get; set; }


    }
}