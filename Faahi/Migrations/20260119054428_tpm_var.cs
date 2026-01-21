using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class tpm_var : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "temp_im_variants",
                columns: table => new
                {
                    temp_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    base_price = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    committed_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temp_im_variants", x => x.temp_variant_id);
                });

            migrationBuilder.CreateIndex(
                name: "company_id",
                table: "temp_im_variants",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "store_id",
                table: "temp_im_variants",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "temp_variant_id",
                table: "temp_im_variants",
                column: "temp_variant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temp_im_variants");
        }
    }
}
