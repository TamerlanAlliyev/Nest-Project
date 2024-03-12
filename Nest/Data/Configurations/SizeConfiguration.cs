using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class SizeConfiguration : BaseEntityConfiguration<Size>
    {
        public override void Configure(EntityTypeBuilder<Size> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(s => s.Count).HasColumnType("int").IsRequired();

            builder.HasKey(m => m.Id);
            builder.ToTable("Sizes");


            builder.HasOne(sw => sw.Product)
                .WithMany(sw => sw.Sizes)
                .HasForeignKey(sw => sw.ProductId);

        }
    }
}
