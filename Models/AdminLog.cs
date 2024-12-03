using Ecommerce_Group_Project.Models;
using System.ComponentModel.DataAnnotations;

public class AdminLog
{
    [Key]
    public int LogID { get; set; }
    public int AdminID { get; set; }
    public User Admin { get; set; }
    public string Action { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
}