﻿namespace Expenses.Domain.Entities
{
    public class RolePermission: EntityBase
    {
        public int RoleId { get; set; }
        public Role Role  { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
