using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class addes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_addresses_am_users_user_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_addresses_mk_customer_profiles_customer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropIndex(
                name: "IX_mk_customer_addresses_customer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropIndex(
                name: "IX_mk_customer_addresses_user_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "customer_profile_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "mk_customer_addresses");

            migrationBuilder.RenameColumn(
                name: "address_line2",
                table: "mk_customer_addresses",
                newName: "Land_mark");

            migrationBuilder.AddColumn<Guid>(
                name: "business_id",
                table: "mk_customer_addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "delevery_end_time",
                table: "mk_customer_addresses",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "delevery_start_time",
                table: "mk_customer_addresses",
                type: "time",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_business_id",
                table: "mk_customer_addresses",
                column: "business_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_addresses_co_business_business_id",
                table: "mk_customer_addresses",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_addresses_co_business_business_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropIndex(
                name: "IX_mk_customer_addresses_business_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "business_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "delevery_end_time",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "delevery_start_time",
                table: "mk_customer_addresses");

            migrationBuilder.RenameColumn(
                name: "Land_mark",
                table: "mk_customer_addresses",
                newName: "address_line2");

            migrationBuilder.AddColumn<Guid>(
                name: "customer_profile_id",
                table: "mk_customer_addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "mk_customer_addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_customer_profile_id",
                table: "mk_customer_addresses",
                column: "customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_user_id",
                table: "mk_customer_addresses",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_addresses_am_users_user_id",
                table: "mk_customer_addresses",
                column: "user_id",
                principalTable: "am_users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_addresses_mk_customer_profiles_customer_profile_id",
                table: "mk_customer_addresses",
                column: "customer_profile_id",
                principalTable: "mk_customer_profiles",
                principalColumn: "customer_profile_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
