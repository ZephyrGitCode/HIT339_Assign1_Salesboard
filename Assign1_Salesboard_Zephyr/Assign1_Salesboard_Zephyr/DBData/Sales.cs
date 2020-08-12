﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.DBData
{
    public class Sales
    {
        public int Id { get; set; }

        [ForeignKey("AspNetUsers")]
        [Display(Name = "Seller")]
        public int Sellerid { get; set; }

        [ForeignKey("Items")]
        [Required]
        public int Itemid { get; set; }





    }
}
