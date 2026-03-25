using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class IndexUpdateInExpenceAndJournalHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UX_gl_JournalHeaders_Business_JournalNo",
                table: "gl_JournalHeaders",
                columns: new[] { "BusinessId", "JournalNo" },
                unique: true,
                filter: "[JournalNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UX_ap_Expenses_Business_ExpenseNo",
                table: "ap_Expenses",
                columns: new[] { "BusinessId", "ExpenseNo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_gl_JournalHeaders_Business_JournalNo",
                table: "gl_JournalHeaders");

            migrationBuilder.DropIndex(
                name: "UX_ap_Expenses_Business_ExpenseNo",
                table: "ap_Expenses");
        }
    }
}
