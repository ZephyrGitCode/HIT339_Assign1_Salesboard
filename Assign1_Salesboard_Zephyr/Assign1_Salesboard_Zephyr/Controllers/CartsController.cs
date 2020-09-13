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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace Assign1_Salesboard_Zephyr.Controllers
{
    public class CartsController : Controller
    {
        private readonly Zephyr_ApplicationContext _context;

        private readonly UserManager<Zephyr_ApplicationUser> _userManager;

        private readonly IHttpContextAccessor _session;

        public CartsController(Zephyr_ApplicationContext context, UserManager<Zephyr_ApplicationUser> userManager, IHttpContextAccessor session)
        {
            _context = context;
            _userManager = userManager;
            _session = session;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var user = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = user;
            ViewBag.Browse = "Browse For Items";
            var cart = _context.Cart
                .Where(m => m.CartId == user);

            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();

            // remove from cart
            var checkCount = _session.HttpContext.Session.GetInt32("cartCount");
            int cartCount = checkCount == null ? 0 : (int)checkCount;
            _session.HttpContext.Session.SetInt32("cartCount", --cartCount);

            return RedirectToAction(nameof(Index));
        }

        // POST: Items/Checkout
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            // Gets buyer's ID
            var user = _userManager.GetUserId(HttpContext.User).ToString();
            ViewBag.UserId = user;

            // Get or Create a cart ID
            var cartId = _session.HttpContext.Session.GetString(user);
            
            // get the cart items
            var carts = _context.Cart
                .Where(c => c.CartId == cartId);

            // create the sales
            foreach (Cart cart in carts.ToList())
            {
                // find the item
                var item = await _context.Item
                    .FirstOrDefaultAsync(m => m.Id == cart.ItemId);

                // update the quantity
                item.Quantity -= cart.Quantity;

                _context.Update(item);

                Sale sale = new Sale { SellerId = item.UserId, BuyerId = user,
                    ItemId = cart.ItemId, TotalPrice = (cart.Quantity*item.Price) };
                _context.Update(sale);

                // Actually coded this myself :) rather than modifying Rohan's Epic code
                _context.Remove(_context.Cart.SingleOrDefault(m => m.Id == cart.Id));
                await _context.SaveChangesAsync();
            }

            // Save the changes
            await _context.SaveChangesAsync();

            // delete cart
            _session.HttpContext.Session.SetString(user, "");
            var checkCount = _session.HttpContext.Session.GetInt32("cartCount");
            int cartCount = checkCount == null ? 0 : (int)checkCount;
            _session.HttpContext.Session.SetInt32("cartCount", --cartCount);

            return RedirectToAction("MyItems", "Items");
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
