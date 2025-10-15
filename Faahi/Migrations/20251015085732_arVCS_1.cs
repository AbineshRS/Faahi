using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class arVCS_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "site_id",
                table: "ar_Customers",
                newName: "company_id");

            migrationBuilder.RenameColumn(
                name: "site_id",
                table: "ap_Vendors",
                newName: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "ar_Customers",
                newName: "site_id");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "ap_Vendors",
                newName: "site_id");
        }
    }
}
