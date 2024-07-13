using Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(p => p.FirstName)
                   .HasMaxLength(25)
                   .IsRequired();

            builder.Property(p => p.LastName)
                   .HasMaxLength(25)
                   .IsRequired();

            builder.Property(p => p.UserName)
                   .HasMaxLength(25)
                   .IsRequired();

            builder.Property(p => p.Password)
                   .IsRequired();
        }
    }
}
