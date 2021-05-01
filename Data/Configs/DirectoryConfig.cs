using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Worktop.Data.Configs
{
    public class DirectoryConfig : IEntityTypeConfiguration<Models.Domain.Directory>
    {
        public void Configure(EntityTypeBuilder<Models.Domain.Directory> builder)
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