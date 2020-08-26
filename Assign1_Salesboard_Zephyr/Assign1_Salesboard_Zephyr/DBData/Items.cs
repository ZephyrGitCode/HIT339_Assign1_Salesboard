﻿using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.DBData
{
    public class Items
    {
        public int Id { get; set; }

        [Display(Name = "Seller"), Required]
        public virtual Zephyr_ApplicationUser Seller { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Between 3 and 60 characters, make it accurate and concise"), Required]
        public string Item { get; set; }

        [Display(Name = "Item Description"), StringLength(255)]
        public string Itemdesc { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$"), StringLength(30)]
        public string Category { get; set; }

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
        public string Itemimage { get; set; }
        

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Postdate { get; set; }

    }
}
