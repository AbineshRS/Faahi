using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class update_indexs_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_created_at",
                table: "st_UserStoreAccess",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_role_id",
                table: "st_UserStoreAccess",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_status",
                table: "st_UserStoreAccess",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_store_id",
                table: "st_UserStoreAccess",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserStoreAccess_user_id",
                table: "st_UserStoreAccess",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserRoles_company_id",
                table: "st_UserRoles",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserRoles_description",
                table: "st_UserRoles",
                column: "description");

            migrationBuilder.CreateIndex(
                name: "IX_st_UserRoles_role_name",
                table: "st_UserRoles",
                column: "role_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_st_UserStoreAccess_created_at",
                table: "st_UserStoreAccess");

            migrationBuilder.DropIndex(
                name: "IX_st_UserStoreAccess_role_id",
                table: "st_UserStoreAccess");

            migrationBuilder.DropIndex(
                name: "IX_st_UserStoreAccess_status",
                table: "st_UserStoreAccess");

            migrationBuilder.DropIndex(
                name: "IX_st_UserStoreAccess_store_id",
                table: "st_UserStoreAccess");

            migrationBuilder.DropIndex(
                name: "IX_st_UserStoreAccess_user_id",
                table: "st_UserStoreAccess");

            migrationBuilder.DropIndex(
                name: "IX_st_UserRoles_company_id",
                table: "st_UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_st_UserRoles_description",
                table: "st_UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_st_UserRoles_role_name",
                table: "st_UserRoles");
        }
    }
}
