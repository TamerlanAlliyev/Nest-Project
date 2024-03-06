using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class CustomerRatingConfiguration : BaseEntityConfiguration<CustomerRating>
    {
        public override void Configure(EntityTypeBuilder<CustomerRating> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Evaluation).HasColumnType("int").IsRequired();
            builder.Property(m => m.Comment).HasColumnType("nvarchar(500)").IsRequired();
            builder.Property(m => m.CustomerId).HasColumnType("int").IsRequired();
            builder.Property(m => m.ProductId).HasColumnType("int").IsRequired();

            builder.HasKey(m=>m.Id);
            builder.ToTable("CustomersRatings");

            builder.HasOne<CustomerRating>()
                 .WithMany()
                 .HasPrincipalKey(m => m.Id)
                 .HasForeignKey(m => m.CustomerId);

            builder.HasOne<CustomerRating>()
                 .WithMany()
                 .HasPrincipalKey(m => m.Id)
                 .HasForeignKey(m => m.ProductId);
        }
    }
}
