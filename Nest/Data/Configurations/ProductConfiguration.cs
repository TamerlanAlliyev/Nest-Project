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
            //builder.Property(m => m.CategoryId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Vendor).HasColumnType("int").IsRequired();

            builder.HasKey(x => x.Id);
            builder.ToTable("Products");

            //builder.HasOne<Category>() 
            //    .WithOne()
            //    .HasForeignKey<Product>(m => m.CategoryId);

            builder.HasOne<Vendor>()
                .WithOne()
                .HasForeignKey<Product>(m => m.VendorId);

        }
    }
}
