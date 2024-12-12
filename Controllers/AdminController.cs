using Ecommerce_Group_Project.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Group_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
      
        private readonly ApplicationContext _context;

        public AdminController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Admin()
        {
            var viewModel = new AdminPanelViewModel
            {
                Products = await _context.Products.Include(p => p.Category).ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                NewProduct = new Product(),
                NewCategory = new Category()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product NewProduct, IFormFile ImageFile)
        {
            // Handle image upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Define the path to save the image
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Set the ImageURL for the new product
                NewProduct.ImageURL = "/images/" + uniqueFileName;
            }
            else
            {
               
                NewProduct.ImageURL = "/images/default.jpg";
            }

            NewProduct.CreatedAt = DateTime.Now;
            NewProduct.UpdatedAt = DateTime.Now;

          
            _context.Products.Add(NewProduct);
            await _context.SaveChangesAsync();

            var viewModel = new AdminPanelViewModel();
            viewModel.Categories = await _context.Categories.ToListAsync();
            viewModel.Products = await _context.Products.Include(p => p.Category).ToListAsync();
            viewModel.NewProduct = NewProduct;
            return View("Admin", viewModel);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = new AdminPanelViewModel
            {
                Products = await _context.Products.Include(p => p.Category).ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                NewProduct = product, // Initialize NewProduct with the found product
                NewCategory = new Category() // Initialize NewCategory
            };
            return View("Admin", viewModel);
        }

        // Edit Product (POST)
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id,Product NewProduct, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Define the path to save the image
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Set the ImageURL for the new product
                NewProduct.ImageURL = "/images/" + uniqueFileName;
            }
            else
            {

                NewProduct.ImageURL = "/images/default.jpg";
            }

            NewProduct.CreatedAt = DateTime.Now;
            NewProduct.UpdatedAt = DateTime.Now;


            _context.Products.Update(NewProduct);
            await _context.SaveChangesAsync();

            var viewModel = new AdminPanelViewModel();
            viewModel.Categories = await _context.Categories.ToListAsync();
            viewModel.Products = await _context.Products.Include(p => p.Category).ToListAsync();
            viewModel.NewProduct = NewProduct;
            return View("Admin", viewModel);
        }

        // Delete Product
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Admin));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category NewCategory)
        {

                NewCategory.CreatedAt = DateTime.Now;
                NewCategory.UpdatedAt = DateTime.Now;
                _context.Categories.Add(NewCategory);
                await _context.SaveChangesAsync();
            
            var viewModel = new AdminPanelViewModel();
            viewModel.Categories = await _context.Categories.ToListAsync();
            viewModel.Products = await _context.Products.Include(p => p.Category).ToListAsync();
            viewModel.NewCategory = NewCategory;


            return View("Admin", viewModel);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var viewModel = new AdminPanelViewModel
            {
                Products = await _context.Products.Include(p => p.Category).ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                NewProduct = new Product(), // Initialize NewProduct
                NewCategory = category // Initialize NewCategory with the found category
            };
            return View("Admin", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(int id, [Bind("CategoryID,Name,Description")] Category NewCategory)
        {
            if (id != NewCategory.CategoryID)
            {
                return NotFound();
            }

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Update the properties of the existing category
            existingCategory.Name = NewCategory.Name;
            existingCategory.Description = NewCategory.Description;
            existingCategory.UpdatedAt = DateTime.Now;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            var viewModel = new AdminPanelViewModel
            {
                Categories = await _context.Categories.ToListAsync(),
                Products = await _context.Products.Include(p => p.Category).ToListAsync(),
                NewCategory = existingCategory
            };

            return View("Admin", viewModel);
        }

        // Delete Category
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Admin));
        }
    }
}
