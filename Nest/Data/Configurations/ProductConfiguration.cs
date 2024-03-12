using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;
using Nest.Models.BaseEntitys;

namespace Nest.Data.Configurations
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Name).HasColumnType("nvarchar(150)").IsRequired();
            builder.Property(m => m.Description).HasColumnType("nvarchar(350)").IsRequired();
            builder.Property(m => m.SellPrice).HasPrecision(18, 2).IsRequired();
            builder.Property(m => m.DiscountPrice).HasPrecision(18, 2).IsRequired();

            builder.HasKey(x => x.Id);
            builder.ToTable("Products");

            builder.HasMany(p => p.Category)
                .WithMany(p => p.Product);

            builder.HasOne(p => p.Vendor)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.VendorId);

            builder.HasMany(p => p.ProductImages)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.CustomerRatings)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.Sizes)
                .WithOne(p=>p.Product) // No navigation property
                .HasForeignKey(p => p.ProductId); // Foreign key property

            builder.HasMany(p => p.Weights)
                .WithOne(p=>p.Product) // No navigation property
                .HasForeignKey(p => p.ProductId); // Foreign key property
        }
    }
}
