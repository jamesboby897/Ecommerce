using Ecommerce_Group_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryID);

        modelBuilder.Entity<ProductImage>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
    //    modelBuilder.Entity<CartItem>()
    //.HasKey(ci => ci.CartItemID);

    //    modelBuilder.Entity<CartItem>()
    //        .Property(ci => ci.CartItemID)
    //        .ValueGeneratedOnAdd();

    //    modelBuilder.Entity<CartItem>()
    //        .HasIndex(ci => new { ci.UserID, ci.ProductID })
    //        .IsUnique();
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

        // Seed Data
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryID = 1, Name = "Laptops", Description = "All kinds of laptops, from gaming to business." },
            new Category { CategoryID = 2, Name = "Smartphones", Description = "Latest smartphones from top brands." },
            new Category { CategoryID = 3, Name = "Accessories", Description = "Electronic accessories including chargers and headphones." }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductID = 1,
                Name = "Dell XPS 13",
                Description = "High-performance laptop with 13-inch display",
                Price = 1200.99m,
                CategoryID = 1,
                ImageURL = "https://example.com/images/dellxps13.jpg"
            },
            new Product
            {
                ProductID = 2,
                Name = "iPhone 14",
                Description = "Latest Apple smartphone with advanced features",
                Price = 999.99m,
                CategoryID = 2,
                ImageURL = "https://example.com/images/iphone14.jpg"
            },
            new Product
            {
                ProductID = 3,
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse with precision tracking",
                Price = 25.99m,
                CategoryID = 3,
                ImageURL = "https://example.com/images/wirelessmouse.jpg"
            }
        );
    }
}