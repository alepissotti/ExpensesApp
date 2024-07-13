using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountPermissionandRolePermissionComplexIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "ExpensesDB",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_AccountPermissions_AccountId",
                schema: "ExpensesDB",
                table: "AccountPermissions");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission",
                schema: "ExpensesDB",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Permission",
                schema: "ExpensesDB",
                table: "AccountPermissions",
                columns: new[] { "AccountId", "PermissionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Role_Permission",
                schema: "ExpensesDB",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Account_Permission",
                schema: "ExpensesDB",
                table: "AccountPermissions");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "ExpensesDB",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPermissions_AccountId",
                schema: "ExpensesDB",
                table: "AccountPermissions",
                column: "AccountId");
        }
    }
}
