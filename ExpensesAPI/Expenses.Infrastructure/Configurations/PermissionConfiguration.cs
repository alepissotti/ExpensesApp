using Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Infrastructure.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(prop => prop.Name)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(prop => prop.Description)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
