using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace assignments.Models

{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }

        // Change to be your model(s) and table(s)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.SKU);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Console> Consoles { get; set; }
        
         public DbSet<Order> Orders {get; set;}

        public DbSet<OrderItem> OrderItems { get; set; }
        
    }

}