using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class updateNewchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_gl_Accounts_BusinessCode",
                table: "gl_Accounts");

            migrationBuilder.CreateIndex(
                name: "UQ_gl_Accounts_BusinessCode",
                table: "gl_Accounts",
                column: "AccountName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_gl_Accounts_BusinessCode",
                table: "gl_Accounts");

            migrationBuilder.CreateIndex(
                name: "UQ_gl_Accounts_BusinessCode",
                table: "gl_Accounts",
                columns: new[] { "CompanyId", "AccountName" },
                unique: true);
        }
    }
}
