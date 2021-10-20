using GestiuneCD.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestiuneCD.Persistence.Config
{
    public class SesiuneConfiguration : IEntityTypeConfiguration<Sesiune>
    {
        public void Configure(EntityTypeBuilder<Sesiune> builder)
        {
            builder.Property(p => p.tipSesiune).IsRequired().HasColumnType("TINYINT");
            builder.Property(p => p.idCD).IsRequired();
            builder.Property(p => p.startDateTime).HasColumnType("DATETIME");
            builder.Property(p => p.endDateTime).HasColumnType("DATETIME");
            builder.HasOne(b => b.cd).WithMany()
                .HasForeignKey(p => p.idCD);
        }
    }
}
