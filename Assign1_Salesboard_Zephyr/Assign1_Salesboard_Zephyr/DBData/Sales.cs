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
        
        [Display(Name = "Seller"), Required]
        public string SellerId { get; set; }

        [Display(Name = "Buyer"), Required]
        public string BuyerId { get; set; }

        [Display(Name = "Total Cost"), Required]
        public double TotalPrice { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Display(Name = "Purchase Time")]
        public DateTime SaleDate { get; set; }

    }
}
