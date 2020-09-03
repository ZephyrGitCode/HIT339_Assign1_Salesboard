using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.DBData
{
    public class Saleitems
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public virtual Item Item { get; set; }

        public int Quantity { get; set; }

    }
}
