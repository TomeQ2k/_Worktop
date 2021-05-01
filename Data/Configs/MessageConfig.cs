using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Worktop.Models.Domain;

namespace Worktop.Data.Configs
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.Sender)
                    .WithMany(s => s.Messages)
                    .HasForeignKey(m => m.SenderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.ChatRoom)
                    .WithMany(c => c.Messages)
                    .HasForeignKey(m => m.ChatRoomId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}