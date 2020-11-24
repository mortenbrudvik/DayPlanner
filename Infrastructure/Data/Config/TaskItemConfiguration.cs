using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired();

            builder.Property(t => t.Status)
                .HasConversion(
                    t => t.Value,
                    t => TaskStatus.FromValue(t));
            
        }
    }
}