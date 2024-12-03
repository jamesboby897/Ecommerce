using System.Data;

namespace Ecommerce_Group_Project.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<AdminLog> AdminLogs { get; set; }
    }
}
