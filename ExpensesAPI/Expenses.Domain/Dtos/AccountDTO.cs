namespace Expenses.Domain.Dtos
{
    public class AccountDTO: BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; } 
        public List<PermissionDTO> Permissions { get; set; }
    }
}
