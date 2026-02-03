using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class taxt_clas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tx_TaxClasses",
                columns: table => new
                {
                    tax_class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tax_class_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    rate_percent = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tx_TaxClasses", x => x.tax_class_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tax_class_id",
                table: "tx_TaxClasses",
                column: "tax_class_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tax_class_name",
                table: "tx_TaxClasses",
                column: "tax_class_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tx_TaxClasses");
        }
    }
}
