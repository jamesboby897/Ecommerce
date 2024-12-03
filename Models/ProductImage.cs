using Ecommerce_Group_Project.Models;
using System.ComponentModel.DataAnnotations;

public class ProductImage
{
    [Key]
    public int ImageID { get; set; }
    public int ProductID { get; set; }
    public Product Product { get; set; }
    public string ImageURL { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}