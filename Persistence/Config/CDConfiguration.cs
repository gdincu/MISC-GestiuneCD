using GestiuneCD.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestiuneCD.Persistence.Config
{
    public class CDConfiguration : IEntityTypeConfiguration<CD>
    {
        public void Configure(EntityTypeBuilder<CD> builder)
        {
            builder.Property(p => p.Nume).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Tip).IsRequired().HasColumnType("TINYINT");
            builder.Property(p => p.DimensiuneMB).IsRequired().HasColumnType("smallint");
        }
    }
}
