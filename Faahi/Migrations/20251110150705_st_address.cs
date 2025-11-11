using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class st_address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "st_StoresAddres",
                columns: table => new
                {
                    store_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    address_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    line1 = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    line2 = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    region = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    postal_code = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    valid_from = table.Column<DateTime>(type: "datetime", nullable: true),
                    valid_to = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_current = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    st_storesstore_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_StoresAddres", x => x.store_address_id);
                    table.ForeignKey(
                        name: "FK_st_StoresAddres_st_stores_st_storesstore_id",
                        column: x => x.st_storesstore_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_address_type",
                table: "st_StoresAddres",
                column: "address_type");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_is_current",
                table: "st_StoresAddres",
                column: "is_current");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_st_storesstore_id",
                table: "st_StoresAddres",
                column: "st_storesstore_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_store_id",
                table: "st_StoresAddres",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoresAddres_valid_from",
                table: "st_StoresAddres",
                column: "valid_from");

            migrationBuilder.AddForeignKey(
                name: "FK_st_StoreCategories_st_stores_store_id",
                table: "st_StoreCategories",
                column: "store_id",
                principalTable: "st_stores",
                principalColumn: "store_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_StoreCategories_st_stores_store_id",
                table: "st_StoreCategories");

            migrationBuilder.DropTable(
                name: "st_StoresAddres");
        }
    }
}
