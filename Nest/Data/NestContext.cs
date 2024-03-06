using Microsoft.EntityFrameworkCore;
using Nest.Models;

namespace Nest.Data
{
    public class NestContext : DbContext
    {
        public NestContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<CustomerRating> CustomerRatings { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<SizeWeight> SizeWeights { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;
    }
}
