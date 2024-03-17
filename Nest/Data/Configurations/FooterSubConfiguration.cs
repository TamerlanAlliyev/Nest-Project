using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Data.Configurations.BaseConfigurations;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public class FooterSubConfiguration: BaseEntityConfiguration<FooterSub>
    {
        public override void Configure(EntityTypeBuilder<FooterSub> builder)
        {
            base.Configure(builder);

            builder.HasKey(fs => fs.Id);
            builder.Property(fs => fs.SubList).HasColumnType("nvarchar(50)").IsRequired();

            builder.HasOne(fh=>fh.FooterHeads)
                .WithMany(fh=>fh.FooterSubs)
                .HasForeignKey(fh=>fh.FooterHeadsId);
        }
    }
}
