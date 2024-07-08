using Expenses.Domain;
using Expenses.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;


namespace Expenses.Infrastructure.Persistence
{
    public class ExpensesDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExpensesDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ExpensesDB");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<EntityBase>();
            
            foreach (var entry in entries) {
                
                if (entry.State == EntityState.Added) { 
                    entry.Entity.CreatedBy = GetAccountId();
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedBy = GetAccountId();
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private int GetAccountId()
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault();
            var claimSub = claim is null ? null : claim.Value;
            int accountId = (claimSub is null) ?0 :Hashid.Decode(claimSub);
            return accountId;
           
        }

        //DbSets
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountPermission> AccountPermissions { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
    }
}
