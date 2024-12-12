using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Group_Project.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public string UserID { get; set; } 

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string BillingAddress { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string CVV { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
