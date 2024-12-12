using Ecommerce_Group_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Group_Project.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationContext _context;

        public CheckoutController(ApplicationContext context)
        {
            _context = context;
        }

        // Display checkout page
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name;

            // Validate user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch user's cart items
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserID == user.Id)
                .ToListAsync();

            if (!cartItems.Any())
            {
                ViewData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            // Calculate total amount and tax
            var totalAmount = cartItems.Sum(item => item.Quantity * item.Product.Price);
            var taxAmount = totalAmount * 0.13m; // 13% tax
            var totalWithTax = totalAmount + taxAmount;

            // Pass data to the view
            ViewData["TotalAmount"] = totalAmount;
            ViewData["TaxAmount"] = taxAmount;
            ViewData["TotalWithTax"] = totalWithTax;
            ViewData["CartItems"] = cartItems;
            ViewData["FirstName"] = user.FirstName; // Populate FirstName
            ViewData["LastName"] = user.LastName;   // Populate LastName

            return View();
        }


        // Handle checkout submission
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(
    string shippingAddress,
    string billingAddress,
    string phone,
    string cardNumber,
    string cvv,
    DateTime expiryDate)
        {
            var userId = User.Identity.Name;

            // Validate user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch cart items
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserID == user.Id)
                .ToListAsync();

            if (!cartItems.Any())
            {
                ViewData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            // Calculate total amount and tax
            var totalAmount = cartItems.Sum(item => item.Quantity * item.Product.Price);
            var taxAmount = totalAmount * 0.13m; // 13% tax
            var totalWithTax = totalAmount + taxAmount;

            // Create a new order
            var order = new Order
            {
                UserID = user.Id,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                FirstName = user.FirstName, // Populate from user data
                LastName = user.LastName,   // Populate from user data
                Phone = phone,
                CardNumber = cardNumber,
                CVV = cvv,
                ExpiryDate = expiryDate,
                TotalAmount = totalWithTax, // Save total with tax in the database
                OrderDate = DateTime.Now,
                OrderDetails = cartItems.Select(item => new OrderDetail
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // Remove cart items after checkout
            _context.CartItems.RemoveRange(cartItems);

            // Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderID });
        }

        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            // Retrieve the order with the specified ID, including related details
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
            {
                return NotFound(); // If the order does not exist, return a 404 page
            }

            return View(order); // Pass the retrieved order to the view
        }

    }
}
