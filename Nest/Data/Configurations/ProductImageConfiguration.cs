using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class ProductImageConfiguration:BaseEntityConfiguration<ProductImage>
    {
        public override void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Url).HasColumnType("varchar(250)").IsRequired();
            builder.Property(m => m.IsMain).HasColumnType("bit");
            builder.Property(m => m.ProductId).HasColumnType("int").IsRequired();

            builder.HasKey(m=>m.Id);
            builder.ToTable("ProductImages");

            builder.HasOne(pi=>pi.Product)
                .WithMany(pi=>pi.ProductImages)
                .HasForeignKey(pi=>pi.ProductId);


            //builder.HasOne<Product>()
            //    .WithMany()
            //    .HasForeignKey(m => m.ProductId);
        }
    }
}
