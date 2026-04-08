using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class om_cust_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_addresses_mk_customer_profiles_mk_customer_profilescustomer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_profiles_am_user_roles_am_user_rolesuser_role_id",
                table: "mk_customer_profiles");

            migrationBuilder.DropIndex(
                name: "IX_mk_customer_addresses_mk_customer_profilescustomer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "mk_customer_profilescustomer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.RenameColumn(
                name: "am_user_rolesuser_role_id",
                table: "mk_customer_profiles",
                newName: "user_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_mk_customer_profiles_am_user_rolesuser_role_id",
                table: "mk_customer_profiles",
                newName: "IX_mk_customer_profiles_user_role_id");

            migrationBuilder.AddColumn<Guid>(
                name: "customer_profile_id",
                table: "mk_customer_addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_customer_profile_id",
                table: "mk_customer_addresses",
                column: "customer_profile_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_addresses_mk_customer_profiles_customer_profile_id",
                table: "mk_customer_addresses",
                column: "customer_profile_id",
                principalTable: "mk_customer_profiles",
                principalColumn: "customer_profile_id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_profiles_am_user_roles_user_role_id",
                table: "mk_customer_profiles",
                column: "user_role_id",
                principalTable: "am_user_roles",
                principalColumn: "user_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_addresses_mk_customer_profiles_customer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_profiles_am_user_roles_user_role_id",
                table: "mk_customer_profiles");

            migrationBuilder.DropIndex(
                name: "IX_mk_customer_addresses_customer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "customer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.RenameColumn(
                name: "user_role_id",
                table: "mk_customer_profiles",
                newName: "am_user_rolesuser_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_mk_customer_profiles_user_role_id",
                table: "mk_customer_profiles",
                newName: "IX_mk_customer_profiles_am_user_rolesuser_role_id");

            migrationBuilder.AddColumn<Guid>(
                name: "mk_customer_profilescustomer_profile_id",
                table: "mk_customer_addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_mk_customer_profilescustomer_profile_id",
                table: "mk_customer_addresses",
                column: "mk_customer_profilescustomer_profile_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_addresses_mk_customer_profiles_mk_customer_profilescustomer_profile_id",
                table: "mk_customer_addresses",
                column: "mk_customer_profilescustomer_profile_id",
                principalTable: "mk_customer_profiles",
                principalColumn: "customer_profile_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_profiles_am_user_roles_am_user_rolesuser_role_id",
                table: "mk_customer_profiles",
                column: "am_user_rolesuser_role_id",
                principalTable: "am_user_roles",
                principalColumn: "user_role_id");
        }
    }
}
