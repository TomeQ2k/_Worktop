using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Configs
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(r => r.UserRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}