using Assign1_Salesboard_Zephyr.Data;
using Assign1_Salesboard_Zephyr.DBData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.ViewModels
{
    public class ItemViewModel
    {
        public List<Item> _items { get; set; }
        /*
        public List<Item> findall()
        {
            _items = new Zephyr_ApplicationContext();
            return _items
        }
        */
        /*
        public List<Item> findAll()
        {
            _items = new List<Item>
        }
        */
    }
}
