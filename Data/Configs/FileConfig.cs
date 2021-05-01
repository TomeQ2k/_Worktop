using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Worktop.Data.Configs
{
    public class FileConfig : IEntityTypeConfiguration<Models.Domain.File>
    {
        public void Configure(EntityTypeBuilder<Models.Domain.File> builder)
        {
            builder.HasOne(f => f.Directory)
                    .WithMany(d => d.Files)
                    .HasForeignKey(f => f.DirectoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.User)
                    .WithMany(u => u.Files)
                    .HasForeignKey(f => f.UserId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}