using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _datut_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_co_business_company_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_co_business_company_id",
                table: "so_SalesLines");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "so_SalesLines",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_company_id",
                table: "so_SalesLines",
                newName: "IX_business_id");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "so_SalesHeaders",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_company_id",
                table: "so_SalesHeaders",
                newName: "IX_business_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_co_business_business_id",
                table: "so_SalesHeaders",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_co_business_business_id",
                table: "so_SalesLines",
                column: "business_id",
                principalTable: "co_business",
                principalColumn: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesHeaders_co_business_business_id",
                table: "so_SalesHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_so_SalesLines_co_business_business_id",
                table: "so_SalesLines");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "so_SalesLines",
                newName: "company_id");

            migrationBuilder.RenameIndex(
                name: "IX_business_id",
                table: "so_SalesLines",
                newName: "IX_company_id");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "so_SalesHeaders",
                newName: "company_id");

            migrationBuilder.RenameIndex(
                name: "IX_business_id",
                table: "so_SalesHeaders",
                newName: "IX_company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesHeaders_co_business_company_id",
                table: "so_SalesHeaders",
                column: "company_id",
                principalTable: "co_business",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_so_SalesLines_co_business_company_id",
                table: "so_SalesLines",
                column: "company_id",
                principalTable: "co_business",
                principalColumn: "company_id");
        }
    }
}
