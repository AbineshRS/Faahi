using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class store_related_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "st_StoreCategories",
                columns: table => new
                {
                    store_category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    is_selected = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_StoreCategories", x => x.store_category_id);
                });

            migrationBuilder.CreateTable(
                name: "st_StoreCategoryTemplates",
                columns: table => new
                {
                    store_category_template_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_type = table.Column<string>(type: "varchar(50)", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_StoreCategoryTemplates", x => x.store_category_template_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategories_category_id",
                table: "st_StoreCategories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategories_is_selected",
                table: "st_StoreCategories",
                column: "is_selected");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategories_store_id",
                table: "st_StoreCategories",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategoryTemplates_category_id",
                table: "st_StoreCategoryTemplates",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_StoreCategoryTemplates_store_type",
                table: "st_StoreCategoryTemplates",
                column: "store_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "st_StoreCategories");

            migrationBuilder.DropTable(
                name: "st_StoreCategoryTemplates");
        }
    }
}
