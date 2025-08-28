using Microsoft.AspNetCore.Mvc;
using BartenderApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BartenderApplication.Controllers
{
    public class CocktailOrderController : Controller
    {
        private readonly BarDbContext _context;

        public CocktailOrderController(BarDbContext context)
        {
            _context = context;
        }

        // Show cocktail menu
        public IActionResult Menu()
        {
            var cocktails = _context.Cocktails.ToList();
            return View(cocktails);
        }

        // Place an order (POST)
        [HttpPost]
        public async Task<IActionResult> Order(int cocktailId, string customerName, int quantity)
        {
            var order = new Order
            {
                CocktailId = cocktailId,
                CustomerName = customerName,
                Quantity = quantity,
                Status = "Pending",
                OrderTime = DateTime.Now
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Confirmation", new { orderId = order.Id });
        }

        // Confirmation view
        public async Task<IActionResult> Confirmation(int orderId)
        {
            var order = await _context.Orders.Include(o => o.Cocktail).FirstOrDefaultAsync(o => o.Id == orderId);
            return View(order);
        }

        // Show bartender order queue
        public async Task<IActionResult> OrderQueue()
        {
            var orders = await _context.Orders.Include(o => o.Cocktail).Where(o => o.Status == "Pending").ToListAsync();
            return View(orders);
        }

        // Mark an order as ready
        [HttpPost]
        public async Task<IActionResult> SetPrepared(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = "Prepared";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("OrderQueue");
        }

        // Show completed (prepared) orders
        public IActionResult CompletedOrders()
        {
            var completedOrders = _context.Orders
                .Include(o => o.Cocktail)
                .Where(o => o.Status == "Ready" || o.Status == "Prepared")
                .ToList();
            return View(completedOrders);
        }

    }
}
