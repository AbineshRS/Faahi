using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class currencys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fx_Currencies",
                columns: table => new
                {
                    currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    country_code = table.Column<string>(type: "char(3)", nullable: false),
                    currency_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    currency_symbol = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fx_Currencies", x => x.currency_id);
                });

            migrationBuilder.CreateTable(
                name: "fx_Timezones",
                columns: table => new
                {
                    timezone_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    timezone = table.Column<string>(type: "varchar(50)", nullable: false),
                    timezone_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    fx_Currenciescurrency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fx_Timezones", x => x.timezone_id);
                    table.ForeignKey(
                        name: "FK_fx_Timezones_fx_Currencies_fx_Currenciescurrency_id",
                        column: x => x.fx_Currenciescurrency_id,
                        principalTable: "fx_Currencies",
                        principalColumn: "currency_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_fx_Currencies_currency_id",
                table: "fx_Currencies",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_fx_Currencies_currency_name",
                table: "fx_Currencies",
                column: "currency_name");

            migrationBuilder.CreateIndex(
                name: "IX_fx_Timezones_fx_Currenciescurrency_id",
                table: "fx_Timezones",
                column: "fx_Currenciescurrency_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fx_Timezones");

            migrationBuilder.DropTable(
                name: "fx_Currencies");
        }
    }
}
