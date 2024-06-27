namespace Expenses.Domain.Entities
{
    public class Permission: EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<AccountPermission> AccountPermissions { get; set; }
        public IEnumerable<RolePermission> RolePermissions { get; set; }
    }
}
