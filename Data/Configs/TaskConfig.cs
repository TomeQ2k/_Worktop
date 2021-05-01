using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Worktop.Models.Domain;

namespace Worktop.Data.Configs
{
    public class TaskConfig : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasOne(t => t.Executor)
                     .WithMany(u => u.Tasks)
                     .HasForeignKey(t => t.ExecutorId)
                     .IsRequired(false)
                     .OnDelete(DeleteBehavior.SetNull);
        }
    }
}