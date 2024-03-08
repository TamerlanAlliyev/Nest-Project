using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class SizeWeightConfiguration : BaseEntityConfiguration<SizeWeight>
    {
        public override void Configure(EntityTypeBuilder<SizeWeight> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Weight).HasColumnType("int").IsRequired();
            builder.Property(m => m.WeightCount).HasColumnType("int").IsRequired();
            builder.Property(m => m.Size).HasColumnType("int").IsRequired();
            builder.Property(m => m.SizeCount).HasColumnType("int").IsRequired();
            builder.Property(m => m.ProductId).HasColumnType("int").IsRequired();

            builder.HasKey(m => m.Id);
            builder.ToTable("SizesWeights");

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(p => p.ProductId);
        }
    }
}
