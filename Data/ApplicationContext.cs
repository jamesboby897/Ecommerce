using Ecommerce_Group_Project.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<AdminLog> AdminLogs { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleID);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryID);

        modelBuilder.Entity<CartItem>()
            .HasKey(ci => new { ci.UserID, ci.ProductID });

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(ci => ci.UserID);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductID);

        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderID, od.ProductID });

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderID);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductID);

        modelBuilder.Entity<AdminLog>()
            .HasOne(al => al.Admin)
            .WithMany(u => u.AdminLogs)
            .HasForeignKey(al => al.AdminID);
    }
}