using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class createnewtablesforexpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ap_Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpenseNo = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    PayeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayeeName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PaymentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ExpenseDate = table.Column<DateTime>(type: "date", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_Expenses", x => x.ExpenseId);
                });

            migrationBuilder.CreateTable(
                name: "ap_ExpenseLines",
                columns: table => new
                {
                    ExpenseLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_ExpenseLines", x => x.ExpenseLineId);
                    table.ForeignKey(
                        name: "FK_ap_ExpenseLines_ap_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "ap_Expenses",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseLines_Expense",
                table: "ap_ExpenseLines",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Business",
                table: "ap_Expenses",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Store",
                table: "ap_Expenses",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ap_ExpenseLines");

            migrationBuilder.DropTable(
                name: "ap_Expenses");
        }
    }
}
