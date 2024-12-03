using Ecommerce_Group_Project.Models;
using System.ComponentModel.DataAnnotations;

public class PaymentTransaction
{
    [Key]
    public int TransactionID { get; set; }
    public int OrderID { get; set; }
    public Order Order { get; set; }
    public string PaymentMethod { get; set; }
    public string TransactionStatus { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTime TransactionDate { get; set; }
}