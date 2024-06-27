namespace Expenses.Domain.Entities
{
    public class AccountPermission: EntityBase
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
