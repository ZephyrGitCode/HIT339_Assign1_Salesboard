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
        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);

            return View(await _context.Item.ToListAsync());
        }

        // GET: My Items
        public ActionResult MyItems()
        {
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier); // Will return userID

            //Zephyr_ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            //string userEmail = applicationUser?.Email; // will give the user's Email
            
            var seller = _userManager.GetUserId(HttpContext.User);
            var items = _context.Item
                .Where(m => m.UserId == seller);

            //return View("Index", items);
            return View("Index",items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
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
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Itemname,Itemdesc,Category,Price,Quantity,Itemimage,Postdate")] Item item)
        {
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
            // Get or Create a cart ID
            string cartId = _session.HttpContext.Session.GetString("cartId");

            if (string.IsNullOrEmpty(cartId) == true) cartId = Guid.NewGuid().ToString();

            // Use the cart id
            cart.CartId = cartId.ToString();

            // Make the sale
            _context.Add(cart);

            // Save the changes
            await _context.SaveChangesAsync();

            // Add to cart
            var checkCount = _session.HttpContext.Session.GetInt32("cartCount");
            int cartCount = checkCount == null ? 0 : (int)checkCount;
            _session.HttpContext.Session.SetString("cartId", cartId.ToString());
            _session.HttpContext.Session.SetInt32("cartCount", ++cartCount);

            return RedirectToAction(nameof(Index));
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
