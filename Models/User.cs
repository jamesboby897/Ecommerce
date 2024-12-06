using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Ecommerce_Group_Project.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
