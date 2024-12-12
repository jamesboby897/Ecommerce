using Ecommerce_Group_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Group_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationContext _context;

        public CartController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name;
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.User)
                .Where(c => c.UserID == userId)
                .ToListAsync();
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = User.Identity.Name;

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserID = userId,
                    ProductID = productId,
                    Quantity = Math.Min(quantity, 99),
                    CreatedAt = DateTime.Now
                };
                await _context.CartItems.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += Math.Min(cartItem.Quantity + quantity, 99);
            }

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, int quantity)
        {
            var userId = User.Identity.Name;
            var cartItem = await _context.CartItems
                 .FirstOrDefaultAsync(c => c.UserID == userId && c.CartItemID == cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = Math.Min(quantity, 99);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = User.Identity.Name;

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserID == userId && c.CartItemID == cartItemId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
