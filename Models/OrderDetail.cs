using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Group_Project.Models
{
    public class OrderDetail
    {

        [Key]
        public int OrderItemID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
