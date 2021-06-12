using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Configs
{
    public class ConnectionConfig : IEntityTypeConfiguration<Connection>
    {
        public void Configure(EntityTypeBuilder<Connection> builder)
        {
            builder.HasKey(c => new { c.UserId, c.ConnectionId });

            builder.HasOne(c => c.ChatRoom)
                    .WithMany(c => c.Connections)
                    .HasForeignKey(c => c.ChatRoomId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.User)
                    .WithMany(u => u.Connections)
                    .HasForeignKey(c => c.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}