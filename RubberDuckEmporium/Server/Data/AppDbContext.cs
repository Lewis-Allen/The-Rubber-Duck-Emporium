using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RubberDuckEmporium.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberDuckEmporium.Server.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<BasketModel> Baskets { get; set; }
        public DbSet<BasketItemModel> BasketITems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Keys ----------------------
            builder.Entity<ProductModel>()
                .HasKey(p => p.ProductID);

            builder.Entity<OrderModel>()
                .HasKey(o => o.OrderID);

            builder.Entity<OrderItemModel>()
                .HasKey(oi => new { oi.OrderID, oi.ProductID });

            builder.Entity<BasketModel>()
                .HasKey(b => b.BasketID);

            builder.Entity<BasketModel>()
                .Property(b => b.BasketID)
                .ValueGeneratedOnAdd();

            builder.Entity<BasketItemModel>()
                .HasKey(bi => bi.BasketItemID);

            // Other Properties ----------
            builder.Entity<ProductModel>()
                .Property(p => p.Name)
                .IsRequired();

            builder.Entity<BasketModel>()
                .HasMany(b => b.BasketItems)
                .WithOne(bi => bi.Basket);

            builder.Entity<OrderModel>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order);
        }
    }
}
