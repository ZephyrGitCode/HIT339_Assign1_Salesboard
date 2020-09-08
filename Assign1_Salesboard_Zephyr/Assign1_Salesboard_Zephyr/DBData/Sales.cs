using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.DBData
{
    public class Sale
    {
        public int Id { get; set; }

        [Display(Name = "Item"), Required]
        public int ItemId { get; set; }
        
        [Display(Name = "Buyer"), Required]
        public string BuyerId { get; set; }
        

        public int Quantity { get; set; }

    }
}
