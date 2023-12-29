using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics;
using POS.DB.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POS.DB.Enums;

namespace POS.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesss { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrdersLine { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Order>()
                .Property(d => d.PaymentMethod)
                .HasConversion(new EnumToStringConverter<PaymentMethod>());
            modelBuilder
                .Entity<Order>()
                .Property(d => d.Status)
                .HasConversion(new EnumToStringConverter<OrderStatus>());
            modelBuilder
                .Entity<Item>()
                .Property(d => d.Type)
                .HasConversion(new EnumToStringConverter<ItemType>());

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate DeleteBehavior

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany()
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate DeleteBehavior

        }

    }
}
