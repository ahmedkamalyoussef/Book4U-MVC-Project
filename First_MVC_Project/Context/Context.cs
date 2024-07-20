using First_MVC_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace First_MVC_Project.Context
{
    public class MyContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        //public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasDefaultValue(string.Empty);
            modelBuilder.Entity<Product>()
            .Property(p => p.ProductUrl)
            .HasDefaultValue(string.Empty);
            modelBuilder.Entity<Product>()
            .Property(p => p.PublisherId)
            .HasDefaultValue(string.Empty);
            modelBuilder.Entity<AppUser>()
            .Property(a=>a.FirstName)
            .HasDefaultValue(string.Empty);
            modelBuilder.Entity<AppUser>()
            .Property(a => a.LastName)
            .HasDefaultValue(string.Empty);
            modelBuilder.Entity<Product>()
            .Property(a => a.Coantity)
            .HasDefaultValue(10);


            base.OnModelCreating(modelBuilder);
        }

    }
}
