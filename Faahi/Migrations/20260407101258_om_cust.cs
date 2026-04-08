using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class om_cust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "mk_customer_addresses",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "mk_customer_addresses",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "om_OrderSources",
                columns: table => new
                {
                    source_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    source_code = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    source_name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "T")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_om_OrderSources", x => x.source_id);
                    table.ForeignKey(
                        name: "FK_om_OrderSources_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_om_OrderSources_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_om_OrderSources_business_store_active",
                table: "om_OrderSources",
                columns: new[] { "business_id", "store_id", "status", "source_name" });

            migrationBuilder.CreateIndex(
                name: "IX_om_OrderSources_store_id",
                table: "om_OrderSources",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "UX_om_OrderSources_business_source_code",
                table: "om_OrderSources",
                columns: new[] { "source_id", "source_code" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "om_OrderSources");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "mk_customer_addresses");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "mk_customer_addresses");
        }
    }
}
