using Ecommerce_Group_Project.Models;
using System.ComponentModel.DataAnnotations;

public class UserAddress
{
    [Key]
    public int AddressID { get; set; }
    public int UserID { get; set; }
    public User User { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}