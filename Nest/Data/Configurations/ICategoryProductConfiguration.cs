using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest.Models;

namespace Nest.Data.Configurations
{
    public interface ICategoryProductConfiguration
    {
        void Configure(EntityTypeBuilder<CategoryProduct> builder);
    }
}