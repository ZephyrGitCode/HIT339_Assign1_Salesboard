using Assign1_Salesboard_Zephyr.Data;
using Assign1_Salesboard_Zephyr.DBData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.ViewModels
{
    public class ItemViewModel
    {
        private readonly Zephyr_ApplicationContext _context;

        public ItemViewModel(Zephyr_ApplicationContext context)
        {
            _context = context;
        }

        public List<Item> _items { get; set; }
        
        public List<Item> findall()
        {
            _items = new List<Item>();
            return _items;
        }
        
        
        public async Task<Item> findAsync(int? id)
        {
            if (id == null)
            {
                //;
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                //return NotFound();
            }
            return item;

        }
        
    }
}
