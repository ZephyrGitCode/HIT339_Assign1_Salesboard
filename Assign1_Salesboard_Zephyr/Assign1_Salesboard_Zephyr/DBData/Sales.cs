using System;
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

        [ForeignKey("Items")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Between 3 and 60 characters, make it accurate and concise"), Required]
        public string Itemname { get; set; }



    }
}
