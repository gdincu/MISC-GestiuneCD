using GestiuneCD.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestiuneCD.Persistence.Config
{
    public class CDConfiguration : IEntityTypeConfiguration<CD>
    {
        public void Configure(EntityTypeBuilder<CD> builder)
        {
            builder.Property(p => p.nume).IsRequired().HasMaxLength(100);
            builder.Property(p => p.tip).IsRequired().HasColumnType("TINYINT");
            builder.Property(p => p.dimensiuneMB).IsRequired().HasColumnType("smallint");
        }
    }
}
