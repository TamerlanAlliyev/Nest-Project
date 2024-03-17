using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class FooterHeadsConfiguration : BaseEntityConfiguration<FooterHeads>
    {
        public override void Configure(EntityTypeBuilder<FooterHeads> builder)
        {
            base.Configure(builder);
            builder.HasKey(fh => fh.Id);
            builder.Property(fh => fh.Head).HasColumnType("nvarchar(50)").IsRequired();

            builder.HasMany(fs => fs.FooterSubs)
                .WithOne(fh => fh.FooterHeads)
                .HasForeignKey(fs => fs.FooterHeadsId);
        }
    }
}
