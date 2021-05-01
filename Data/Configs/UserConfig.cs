using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Worktop.Models.Domain;

namespace Worktop.Data.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.UserRoles)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Job)
                    .WithMany(j => j.Users)
                    .HasForeignKey(u => u.JobId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Opinions)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.ChatRooms)
                    .WithOne(c => c.Creator)
                    .HasForeignKey(c => c.CreatorId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.MailsSent)
                    .WithOne(m => m.Sender)
                    .HasForeignKey(m => m.SenderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.MailsReceived)
                    .WithOne(m => m.Receiver)
                    .HasForeignKey(m => m.ReceiverId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}