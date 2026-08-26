using SmartTodo.Domain.Todos;
using SmartTodo.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartTodo.Infrastructure.Todos.Persistence;

public class TodoConfigurations : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Todos");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.IsCompleted)
            .IsRequired();
        
        builder.Property(t => t.CreatedAt)
            .IsRequired();
        
        builder.Property(s => s.AdminId);

        
        // Configure TodoType as value object (if it's a SmartEnum)
        builder.Property(t => t.TodoType)
            .HasConversion(
                todoType => todoType.Value,  // Store the int value
                value => TodoType.FromValue(value)  // Convert back to enum
            )
            .IsRequired();
        
    }
}
