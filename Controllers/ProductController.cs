using Ecommerce_Group_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce_Group_Project.Controllers
{

        public class ProductController : Controller
    {
        private readonly ApplicationContext _context;

        public ProductController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound("Product ID is required.");
            }

            var product = await _context.Products
                .Include(p => p.ProductImages) // Include related images if necessary
                .Include(p => p.Category) // Include category if necessary
                .FirstOrDefaultAsync(p => p.ProductID == id);

            //if (product == null)
            //{
            //    return NotFound($"No product found with ID {id}.");
            //}

            if (product == null)
            {
                return View("404"); // Render the custom error view
            }

            return View(product);
        }
    }
}