namespace Ecommerce_Group_Project.Models
{
    public class AdminPanelViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public Product NewProduct { get; set; }
        public Category NewCategory { get; set; }
    }
}
