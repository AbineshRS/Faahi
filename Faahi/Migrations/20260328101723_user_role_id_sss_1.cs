using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class user_role_id_sss_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "store_user_id",
                table: "am_user_roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_am_user_roles_store_user_id",
                table: "am_user_roles",
                column: "store_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_am_user_roles_st_Users_store_user_id",
                table: "am_user_roles",
                column: "store_user_id",
                principalTable: "st_Users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_am_user_roles_st_Users_store_user_id",
                table: "am_user_roles");

            migrationBuilder.DropIndex(
                name: "IX_am_user_roles_store_user_id",
                table: "am_user_roles");

            migrationBuilder.DropColumn(
                name: "store_user_id",
                table: "am_user_roles");
        }
    }
}
