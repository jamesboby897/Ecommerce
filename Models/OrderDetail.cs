namespace Ecommerce_Group_Project.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}