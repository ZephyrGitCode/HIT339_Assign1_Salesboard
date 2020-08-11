using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.Models
{
    public class Items
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3), Required]
        public string Itemname { get; set; }

        [Display(Name = "Item Description"), StringLength(255)]
        public string Itemdesc { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$"), Required, StringLength(30)]
        public string Category { get; set; }

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [StringLength(255)]
        public IFormFile Itemimage { get; set; }

        //Make this user_id
        public string Seller { get; set; }
    }
}
