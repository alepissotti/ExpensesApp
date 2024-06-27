namespace Expenses.Domain.Entities
{
    public class Role: EntityBase
    {
        public string Name { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<RolePermission> RolePermissions { get; set; }
    }
}
