using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class zon_related_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "business_id",
                table: "mk_blacklisted_numbers",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_mk_blacklisted_numbers_business_id",
                table: "mk_blacklisted_numbers",
                column: "business_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mk_blacklisted_numbers_co_business_business_id",
                table: "mk_blacklisted_numbers",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mk_blacklisted_numbers_co_business_business_id",
                table: "mk_blacklisted_numbers");

            migrationBuilder.DropIndex(
                name: "IX_mk_blacklisted_numbers_business_id",
                table: "mk_blacklisted_numbers");

            migrationBuilder.DropColumn(
                name: "business_id",
                table: "mk_blacklisted_numbers");
        }
    }
}
