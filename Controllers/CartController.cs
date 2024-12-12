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

        // Display the shopping cart
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name;

            // Validate the user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user not found
            }

            // Fetch cart items for the user
            var cartItems = await _context.CartItems
                .Include(c => c.Product) // Include Product details
                .Where(c => c.UserID == user.Id)
                .ToListAsync();

            return View(cartItems);
        }

        // Add item to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = User.Identity.Name;

            // Validate the user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user not found
            }

            // Check if the product is already in the cart
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserID == user.Id && c.ProductID == productId);

            if (cartItem == null)
            {
                // Add a new cart item
                cartItem = new CartItem
                {
                    UserID = user.Id,
                    ProductID = productId,
                    Quantity = Math.Min(quantity, 99),
                    CreatedAt = DateTime.Now
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                // Update the quantity of an existing cart item
                cartItem.Quantity += quantity;
                cartItem.Quantity = Math.Min(cartItem.Quantity, 99); // Cap at 99
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect back to the cart page
            return RedirectToAction("Index", "Cart");
        }

        // Update the quantity of a cart item
        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, int quantity)
        {
            // Validate quantity range
            if (quantity < 1 || quantity > 10)
            {
                ViewData["ErrorMessage"] = "Quantity must be between 1 and 10.";
                return RedirectToAction("Index");
            }

            // Find the cart item
            var cartItem = await _context.CartItems
                .Include(c => c.Product) // Include product details for stock validation
                .FirstOrDefaultAsync(c => c.CartItemID == cartItemId);

            if (cartItem == null)
            {
                ViewData["ErrorMessage"] = "Cart item not found.";
                return RedirectToAction("Index");
            }

            // Update the quantity
            cartItem.Quantity = quantity;
            _context.CartItems.Update(cartItem);

            // Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        // Remove item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.CartItemID == cartItemId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
