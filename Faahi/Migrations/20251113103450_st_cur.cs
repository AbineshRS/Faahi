using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class st_cur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "default_close_time",
                table: "st_stores",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.CreateTable(
                name: "st_store_currencies",
                columns: table => new
                {
                    store_currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    currency_code = table.Column<string>(type: "char(3)", maxLength: 3, nullable: true),
                    is_default = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    st_StoresAddresstore_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_store_currencies", x => x.store_currency_id);
                    table.ForeignKey(
                        name: "FK_st_store_currencies_st_StoresAddres_st_StoresAddresstore_address_id",
                        column: x => x.st_StoresAddresstore_address_id,
                        principalTable: "st_StoresAddres",
                        principalColumn: "store_address_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_currency_code",
                table: "st_store_currencies",
                column: "currency_code");

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_st_StoresAddresstore_address_id",
                table: "st_store_currencies",
                column: "st_StoresAddresstore_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_store_currency_id",
                table: "st_store_currencies",
                column: "store_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_store_currencies_store_id",
                table: "st_store_currencies",
                column: "store_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "st_store_currencies");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "default_close_time",
                table: "st_stores",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);
        }
    }
}
