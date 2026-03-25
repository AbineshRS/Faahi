using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class AddChequesAndAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ap_Cheques",
                columns: table => new
                {
                    ChequeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PayeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayeeName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PaymentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_Cheques", x => x.ChequeId);
                });

            migrationBuilder.CreateTable(
                name: "ap_ExpensesAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_ExpensesAttachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_ap_ExpensesAttachments_ap_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "ap_Expenses",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_ChequeLines",
                columns: table => new
                {
                    ChequeLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChequeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_ChequeLines", x => x.ChequeLineId);
                    table.ForeignKey(
                        name: "FK_ap_ChequeLines_ap_Cheques_ChequeId",
                        column: x => x.ChequeId,
                        principalTable: "ap_Cheques",
                        principalColumn: "ChequeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_ChequesAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChequeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_ChequesAttachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_ap_ChequesAttachments_ap_Cheques_ChequeId",
                        column: x => x.ChequeId,
                        principalTable: "ap_Cheques",
                        principalColumn: "ChequeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChequeLines_Cheque",
                table: "ap_ChequeLines",
                column: "ChequeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_Business",
                table: "ap_Cheques",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_Store",
                table: "ap_Cheques",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ap_ChequesAttachments_Cheque",
                table: "ap_ChequesAttachments",
                columns: new[] { "ChequeId", "UploadedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ap_ExpensesAttachments_Expense",
                table: "ap_ExpensesAttachments",
                columns: new[] { "ExpenseId", "UploadedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ap_ChequeLines");

            migrationBuilder.DropTable(
                name: "ap_ChequesAttachments");

            migrationBuilder.DropTable(
                name: "ap_ExpensesAttachments");

            migrationBuilder.DropTable(
                name: "ap_Cheques");
        }
    }
}
