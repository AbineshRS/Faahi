using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class addes_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "delevery_date",
                table: "mk_customer_addresses",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mk_business_zones",
                columns: table => new
                {
                    zone_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    zone_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_active = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mk_business_zones", x => x.zone_id);
                    table.ForeignKey(
                        name: "FK_mk_business_zones_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_mk_business_zones_business_id",
                table: "mk_business_zones",
                column: "business_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mk_business_zones");

            migrationBuilder.DropColumn(
                name: "delevery_date",
                table: "mk_customer_addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "business_id",
                table: "mk_customer_addresses",
                type: "uniqueidentifier",
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
    }
}
