using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class pyas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "so_payment_type",
                columns: table => new
                {
                    PayTypeCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(35)", nullable: true),
                    Bank_pcnt = table.Column<decimal>(type: "decimal(14,2)", nullable: true),
                    is_avilable = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true, defaultValue: "T"),
                    cash_types = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    card_type = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    req_det = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_so_payment_type", x => x.PayTypeCode);
                    table.ForeignKey(
                        name: "FK_so_payment_type_co_business_company_id",
                        column: x => x.company_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_so_payment_type_company_id",
                table: "so_payment_type",
                column: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "so_payment_type");
        }
    }
}
