using Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Infrastructure.Configurations
{
    public class AccountPermissionConfiguration : IEntityTypeConfiguration<AccountPermission>
    {
        public void Configure(EntityTypeBuilder<AccountPermission> builder)
        {
            builder.HasIndex(x => new { x.AccountId, x.PermissionId })
                .HasDatabaseName("IX_Account_Permission")
                .IsUnique();
        }
    }
}
