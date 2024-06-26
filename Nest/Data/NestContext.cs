﻿using Microsoft.EntityFrameworkCore;
using Nest.Models;
using Nest.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Nest.Data
{
    public class NestContext : IdentityDbContext<AppUser>
    {
        public NestContext(DbContextOptions<NestContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<CustomerRating> CustomerRatings { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;
        public DbSet<Size> Sizes { get; set; } = null!;
        public DbSet<ProductSize> ProductSize { get; set; } = null!;
        public DbSet<Weight> Weights { get; set; } = null!;
        public DbSet<ProductWeight> ProductWeights { get; set; } = null!;

        public DbSet<FooterHeads> FooterHeads { get; set; } = null!;
        public DbSet<FooterSub> FooterSubs { get; set; } = null!;
        public DbSet<CategoryProduct> CategoryProduct { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NestContext).Assembly);
        }
    }
}
