using Microsoft.AspNetCore.Authorization;

namespace Expenses.Application.Attributes
{
    public class AuthorizePermissionAttribute: AuthorizeAttribute
    {
        public string Permission {  get; }

        public AuthorizePermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
