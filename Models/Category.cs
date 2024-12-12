using System.ComponentModel.DataAnnotations;
namespace Ecommerce_Group_Project.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}