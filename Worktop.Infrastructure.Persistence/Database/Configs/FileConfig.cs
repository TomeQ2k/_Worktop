using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Configs
{
    public class FileConfig : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
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