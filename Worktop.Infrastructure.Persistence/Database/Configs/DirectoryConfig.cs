using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Configs
{
    public class DirectoryConfig : IEntityTypeConfiguration<Directory>
    {
        public void Configure(EntityTypeBuilder<Directory> builder)
        {
            builder.HasOne(d => d.User)
                    .WithMany(u => u.Directories)
                    .HasForeignKey(d => d.UserId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(d => d.Subdirectories)
                    .WithOne(d => d.ParentDirectory)
                    .HasForeignKey(d => d.ParentDirectoryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}