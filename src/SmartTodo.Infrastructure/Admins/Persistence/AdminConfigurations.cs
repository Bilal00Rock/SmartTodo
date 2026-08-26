using SmartTodo.Domain.Admins;
using SmartTodo.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartTodo.Infrastructure.Admins.Persistence;

public class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
         builder.ToTable("Admins");
        
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id)
            .ValueGeneratedNever();
        
        builder.Property(a => a.UserId)
            .IsRequired();
        
         // Use your ListOfIdsConverter for better performance
        builder.Property(a => a.TodoIds)
            .HasListOfIdsConverter()
            .HasColumnType("TEXT")
            .HasColumnName("TodoIds");
        
         builder.HasData(new Admin(
            userId: Guid.Parse("11111111-1111-1111-1111-111111111111"), // Static test GUID
            id: Guid.Parse("2150e333-8fdc-42a3-9474-1a3956d46de8")));
    }
}
