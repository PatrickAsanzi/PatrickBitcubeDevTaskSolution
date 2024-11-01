using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    public class BitcubeDevTaskDbContext : IdentityDbContext<ApplicationUser>
    {
        public BitcubeDevTaskDbContext(DbContextOptions<BitcubeDevTaskDbContext> options)
          : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Make sure to call this!

            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserId);
        }
    }

    public class BitcubeDevTaskDbContextFactory : IDesignTimeDbContextFactory<BitcubeDevTaskDbContext>
    {
        public BitcubeDevTaskDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BitcubeDevTaskDbContext>();
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\patri\\source\\repos\\BitcubeDeveloperTaskSolutionAPI\\Infrastructure\\Data\\db.sqlite");

            return new BitcubeDevTaskDbContext(optionsBuilder.Options);
        }
    }
}