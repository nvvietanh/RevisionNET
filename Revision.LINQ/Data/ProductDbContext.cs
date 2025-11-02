using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Revision.LINQ.Models;

namespace Revision.LINQ.Data
{
    /// <summary>
    /// DbContext cho hệ thống quản lý sản phẩm
    /// Kết nối đến SQL Server để demo LINQ to Entities
    /// </summary>
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đọc connection string từ appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("SchoolDatabase");
                optionsBuilder.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Stock).IsRequired();
            });

            // Seed dữ liệu mẫu
            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Laptop Dell XPS 13", "Điện tử", 25000000, 15),
                new Product(2, "iPhone 15 Pro Max", "Điện tử", 30000000, 8),
                new Product(3, "Bàn phím cơ Keychron K2", "Điện tử", 2500000, 25),
                new Product(4, "Chuột Logitech MX Master 3", "Điện tử", 2000000, 30),
                new Product(5, "Sách: Clean Code", "Sách", 250000, 50),
                new Product(6, "Sách: Design Patterns", "Sách", 300000, 35),
                new Product(7, "Tai nghe Sony WH-1000XM5", "Điện tử", 8000000, 12),
                new Product(8, "Màn hình LG 27 inch 4K", "Điện tử", 7500000, 10),
                new Product(9, "Webcam Logitech C920", "Điện tử", 1800000, 20),
                new Product(10, "Sách: Refactoring", "Sách", 280000, 40)
            );
        }
    }
}
