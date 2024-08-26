using Microsoft.EntityFrameworkCore;
using System;

namespace Carl_Assignment.Entity
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
               .HasKey(s => s.ProductId);

            modelBuilder.Entity<Product>()
               .Property(p => p.ProductId)
               .HasDefaultValueSql("NEXT VALUE FOR ProductID_Sequence");

            modelBuilder.Entity<Product>()
                .Property(s => s.Stock)
                .IsRequired();
                

            modelBuilder.Entity<Product>()
                .Property(s => s.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Product>()
                .Property(s => s.Description)
                .IsRequired(false);

            modelBuilder.Entity<Product>()
                  .Property(s => s.Createdon)
                  .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Product>().HasData(new Product
            {ProductId = 100000,ProductName = "Chair", Category = "Furniture", Stock = 100, Description = "New Furnitures"},
            new Product { ProductId = 100001, ProductName = "Shirt", Category = "Clothes", Stock = 100, Description = "New Clothes"}
            );
        }
        public DbSet<Product> Product { get; set; }
    }
}
