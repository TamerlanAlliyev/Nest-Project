using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(m => m.Icon).HasColumnType("varchar(200)").IsRequired();
            //builder.Property(m=>m.ProductId).HasColumnType("int").IsRequired();

            builder.HasKey(m=>m.Id);
            builder.ToTable("Categories");

            //builder.HasOne<Product>()
            //    .WithMany()
            //    .HasPrincipalKey(m => m.Id)
            //    .HasForeignKey(m => m.ProductId);

            //builder.HasMany(c => c.Product)
            //    .WithMany(c => c.Category);
                

        }
    }
}
