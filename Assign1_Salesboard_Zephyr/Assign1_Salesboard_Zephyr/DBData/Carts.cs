using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.DBData
{
    public class Cart
    {
        public int Id { get; set; }

        [Display(Name = "Cart ID"), Required]
        public string CartId { get; set; }

        [Display(Name = "Item ID"), Required]
        public int ItemId { get; set; }

        public int Quantity { get; set; }

    }
}
