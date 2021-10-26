using GestiuneCD.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestiuneCD.Persistence.Config
{
    public class SesiuneConfiguration : IEntityTypeConfiguration<Sesiune>
    {
        public void Configure(EntityTypeBuilder<Sesiune> builder)
        {
            builder.Property(p => p.TipSesiune).IsRequired().HasColumnType("TINYINT");
            builder.Property(p => p.IdCD).IsRequired();
            builder.Property(p => p.StartDateTime).HasColumnType("DATETIME");
            builder.Property(p => p.EndDateTime).HasColumnType("DATETIME");
            builder.HasOne(b => b.Cd).WithMany()
                .HasForeignKey(p => p.IdCD);
        }
    }
}
