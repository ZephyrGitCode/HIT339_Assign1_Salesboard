using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
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

        public virtual Items Item { get; set; }

        public virtual Zephyr_ApplicationUser Buyer { get; set; }

        public int Quantity { get; set; }

    }
}
