using Expenses.Domain.Entities;

namespace Expenses.Domain.Dtos
{
    public class AccountPermissionDTO: BaseDTO
    {
        public int AccountId { get; set; }
        public int PermissionId { get; set; }
        public PermissionDTO Permission { get; set; }
    }
}
