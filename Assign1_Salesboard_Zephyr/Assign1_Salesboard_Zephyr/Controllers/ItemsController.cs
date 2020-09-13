using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assign1_Salesboard_Zephyr;
using Assign1_Salesboard_Zephyr.DBData;
using Assign1_Salesboard_Zephyr.Data;
using Assign1_Salesboard_Zephyr.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Assign1_Salesboard_Zephyr.ViewModels;

namespace Assign1_Salesboard_Zephyr.Controllers
{
    [ActivatorUtilitiesConstructor]
    public class ItemsController : Controller
    {
        private readonly Zephyr_ApplicationContext _context;

        private readonly UserManager<Zephyr_ApplicationUser> _userManager;

        private readonly IHttpContextAccessor _session;

        public ItemsController(Zephyr_ApplicationContext context, UserManager<Zephyr_ApplicationUser> userManager, IHttpContextAccessor session)
        {
            _context = context;
            _userManager = userManager;
            _session = session;
        }

        [AllowAnonymous]
        // GET: Items
        public async Task<IActionResult> Index(string searchString, string categories)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> categoryQuery = from m in _context.Item 
                                               orderby m.Category 
                                               select m.Category;

            var items = _context.Item
                .Where(m => m.Quantity >= 1);

            // Sorts Items by name
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Itemname.Contains(searchString));
            }

            // Sorts Items by Category
            if (!string.IsNullOrEmpty(categories))
            {
                items = items.Where(x => x.Category == categories);
            }

            // New instance of view model, allows multiple models in 1 page
            var StoreVM = new StoreViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Items = await items.ToListAsync()
            };

            var seller = _userManager.GetUserId(HttpContext.User);
            var isadmin = _userManager.GetUserName(HttpContext.User);
            if (isadmin == "Admin@Admin")
            {
                ViewBag.IsAdmin = true;
            }
            ViewBag.UserId = seller;
            ViewBag.Browse = "Browse For Items";

            return View(StoreVM);
        }

        // GET: My Home Page, returns all items the current has sold and all sales they have made.
        public async Task<ActionResult> MyItems(string categories, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> categoryQuery = from m in _context.Item 
                                               orderby m.Category 
                                               select m.Category;

            // Get current user
            var user = _userManager.GetUserId(HttpContext.User);

            // Return Items that match current user
            var items = _context.Item
                .Where(m => m.UserId == user);

            // Return Sales that match current user
            var allsales = _context.Sale
                .Where(s => s.SellerId == user || s.BuyerId == user);

            // Filter just the current user's sales
            var mysales = _context.Sale
                .Where(s => s.SellerId == user);

            // Filter just the current user's purchases
            var mypurchases = _context.Sale
                .Where(s => s.BuyerId == user);

            // Total of user's sales
            double mysalestotal = mysales.Sum(x => x.TotalPrice);
            ViewBag.SaleTotal = mysalestotal;

            // Total of user's purchases
            double purchasestotal = mypurchases.Sum(x => x.TotalPrice);
            ViewBag.PurchaseTotal = purchasestotal;

            // Sorts Items by name
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Itemname.Contains(searchString));
            }

            // Sorts Items by Category
            if (!string.IsNullOrEmpty(categories))
            {
                items = items.Where(x => x.Category == categories);
            }

            // Order Items by quantity
            var ordereditems = items.OrderByDescending(d => d.Quantity);

            // Order Sales by Date
            var orderedsales = allsales.OrderByDescending(d => d.SaleDate);

            // New instance of view model, allows multiple models in 1 page
            var MyItemsVM = new MyItemsViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Items = await ordereditems.ToListAsync(),
                Sales = await orderedsales.ToListAsync()
            };

            ViewBag.Browse = "Manage Your Items";
            ViewBag.UserId = user;

            //return View("Index", items);
            return View(MyItemsVM);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            var isadmin = _userManager.GetUserName(HttpContext.User);
            if (isadmin == "Admin@Admin")
            {
                ViewBag.IsAdmin = true;
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User); // Returns UserID to place in hidden field for submission
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Itemname,Itemdesc,Category,Price,Quantity,Itemimage,Postdate")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.SellerId = item.UserId;

            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Itemname,Itemdesc,Category,Price,Quantity,Itemimage,Postdate")] Item item)
        {
            var seller = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = seller;

            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Purchase/5
        public async Task<IActionResult> Purchase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Items/Purchase/5
        [HttpPost, ActionName("Purchase")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseConfirmed([Bind("Quantity,ItemId")] Cart cart)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            // Get or Create a cart ID 
            string cartId = _session.HttpContext.Session.GetString(user);

            // Sets cartID to user's string so each user has a unique cart
            if (string.IsNullOrEmpty(cartId) == true) cartId = user;

            // Use the cart id
            cart.CartId = cartId.ToString();

            // Make the sale
            _context.Add(cart);

            // Save the changes
            await _context.SaveChangesAsync();

            // Add to cart
            var checkCount = _session.HttpContext.Session.GetInt32("cartCount");
            int cartCount = checkCount == null ? 0 : (int)checkCount;
            _session.HttpContext.Session.SetString(user, cartId.ToString());
            _session.HttpContext.Session.SetInt32("cartCount", ++cartCount);
            
            return RedirectToAction(nameof(Index),"Carts");
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
