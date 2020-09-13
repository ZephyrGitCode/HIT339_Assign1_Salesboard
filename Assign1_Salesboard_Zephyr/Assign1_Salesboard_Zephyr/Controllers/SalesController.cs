using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assign1_Salesboard_Zephyr.DBData;
using Assign1_Salesboard_Zephyr.Data;
using Microsoft.AspNetCore.Identity;
using Assign1_Salesboard_Zephyr.Areas.Identity.Data;

namespace Assign1_Salesboard_Zephyr.Controllers
{
    public class SalesController : Controller
    {
        private readonly Zephyr_ApplicationContext _context;
        private readonly UserManager<Zephyr_ApplicationUser> _userManager;

        public SalesController(Zephyr_ApplicationContext context, UserManager<Zephyr_ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Sales
        public IActionResult Index()
        {
            var buyer = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = buyer;
            var sales = _context.Sale
                .Where(m => m.BuyerId == buyer);
            var salesdecending = sales.OrderByDescending(d => d.SaleDate);

            return View(salesdecending);
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        private bool SaleExists(int id)
        {
            return _context.Sale.Any(e => e.Id == id);
        }
    }
}
