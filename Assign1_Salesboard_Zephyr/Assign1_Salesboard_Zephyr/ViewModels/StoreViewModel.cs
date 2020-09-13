using Assign1_Salesboard_Zephyr.Data;
using Assign1_Salesboard_Zephyr.DBData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1_Salesboard_Zephyr.ViewModels
{
    public class StoreViewModel
    {
        public List<Item> Items { get; set; }
        public SelectList Categories { get; set; }
        public string SearchString { get; set; }
    }
}
