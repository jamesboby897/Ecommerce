namespace Ecommerce_Group_Project.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}