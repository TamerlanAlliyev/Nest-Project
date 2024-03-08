using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;
using Nest.Models.BaseEntitys;

namespace Nest.Data.Configurations
{
    public class VendorConfiguration:BaseEntityConfiguration<Vendor>
    {
        public override void Configure(EntityTypeBuilder<Vendor> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.FullName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(m => m.MailAddress).HasColumnType("varchar(150)").IsRequired();
            builder.Property(m => m.Password).HasColumnType("varchar(70)").IsRequired();
            builder.Property(m => m.DateOfBirth).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.LivingAddress).HasColumnType("varchar(50)").IsRequired();
            builder.Property(m => m.PhoneNumber).HasColumnType("varchar(20)").IsRequired();
            builder.Property(m => m.Image).HasColumnType("varchar(max)").IsRequired();

            builder.HasKey(m=>m.Id);
            builder.ToTable("Vendors");
        }
    }
}
