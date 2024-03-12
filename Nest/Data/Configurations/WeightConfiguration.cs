using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class WeightConfiguration : BaseEntityConfiguration<Weight>
    {
        public override void Configure(EntityTypeBuilder<Weight> builder)
        {
            base.Configure(builder);

            builder.Property(w => w.Gram).HasColumnType("int").IsRequired();
            builder.Property(w => w.Count).HasColumnType("int").IsRequired();

            builder.HasKey(m => m.Id);
            builder.ToTable("Weights");


            builder.HasOne(sw => sw.Product)
                .WithMany(sw => sw.Weights)
                .HasForeignKey(sw => sw.ProductId);

        }
    }
}