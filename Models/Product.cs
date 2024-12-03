namespace Ecommerce_Group_Project.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public string ImageURL { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}