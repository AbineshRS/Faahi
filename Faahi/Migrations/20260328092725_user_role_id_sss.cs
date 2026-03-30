using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class user_role_id_sss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_am_user_business_access_am_user_roles_am_user_rolesuser_role_id",
                table: "am_user_business_access");

            migrationBuilder.DropForeignKey(
                name: "FK_am_user_roles_am_roles_am_rolesrole_id",
                table: "am_user_roles");

            migrationBuilder.RenameColumn(
                name: "am_rolesrole_id",
                table: "am_user_roles",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_am_user_roles_am_rolesrole_id",
                table: "am_user_roles",
                newName: "IX_am_user_roles_role_id");

            migrationBuilder.RenameColumn(
                name: "am_user_rolesuser_role_id",
                table: "am_user_business_access",
                newName: "user_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_am_user_business_access_am_user_rolesuser_role_id",
                table: "am_user_business_access",
                newName: "IX_am_user_business_access_user_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_am_user_business_access_am_user_roles_user_role_id",
                table: "am_user_business_access",
                column: "user_role_id",
                principalTable: "am_user_roles",
                principalColumn: "user_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_am_user_roles_am_roles_role_id",
                table: "am_user_roles",
                column: "role_id",
                principalTable: "am_roles",
                principalColumn: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_am_user_business_access_am_user_roles_user_role_id",
                table: "am_user_business_access");

            migrationBuilder.DropForeignKey(
                name: "FK_am_user_roles_am_roles_role_id",
                table: "am_user_roles");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "am_user_roles",
                newName: "am_rolesrole_id");

            migrationBuilder.RenameIndex(
                name: "IX_am_user_roles_role_id",
                table: "am_user_roles",
                newName: "IX_am_user_roles_am_rolesrole_id");

            migrationBuilder.RenameColumn(
                name: "user_role_id",
                table: "am_user_business_access",
                newName: "am_user_rolesuser_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_am_user_business_access_user_role_id",
                table: "am_user_business_access",
                newName: "IX_am_user_business_access_am_user_rolesuser_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_am_user_business_access_am_user_roles_am_user_rolesuser_role_id",
                table: "am_user_business_access",
                column: "am_user_rolesuser_role_id",
                principalTable: "am_user_roles",
                principalColumn: "user_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_am_user_roles_am_roles_am_rolesrole_id",
                table: "am_user_roles",
                column: "am_rolesrole_id",
                principalTable: "am_roles",
                principalColumn: "role_id");
        }
    }
}
