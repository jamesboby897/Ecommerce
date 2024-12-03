namespace Ecommerce_Group_Project.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}