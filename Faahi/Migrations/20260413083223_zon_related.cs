using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class zon_related : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "zone_name",
                table: "om_CustomerOrders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "zone_id",
                table: "mk_customer_addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_zone_id",
                table: "mk_customer_addresses",
                column: "zone_id");

            migrationBuilder.AddForeignKey(
                name: "FK_mk_customer_addresses_mk_business_zones_zone_id",
                table: "mk_customer_addresses",
                column: "zone_id",
                principalTable: "mk_business_zones",
                principalColumn: "zone_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mk_customer_addresses_mk_business_zones_zone_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropIndex(
                name: "IX_mk_customer_addresses_zone_id",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "zone_name",
                table: "om_CustomerOrders");

            migrationBuilder.DropColumn(
                name: "zone_id",
                table: "mk_customer_addresses");
        }
    }
}
