using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class Updating_accounts_related_tables_with_dbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ap_Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(30)", nullable: false),
                    PaymentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ChequeDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClearedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(28,12)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "varchar(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "fin_BankDeposits",
                columns: table => new
                {
                    DepositId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepositNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "varchar(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fin_BankDeposits", x => x.DepositId);
                });

            migrationBuilder.CreateTable(
                name: "gl_BusinessSettings",
                columns: table => new
                {
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ar_control_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ap_control_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    inventory_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    cogs_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sales_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tax_payable_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_BusinessSettings", x => x.business_id);
                    table.ForeignKey(
                        name: "FK_gl_BusinessSettings_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gl_JournalHeaders",
                columns: table => new
                {
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JournalDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JournalNo = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    SourceType = table.Column<string>(type: "varchar(30)", nullable: true),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JournalMemo = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_JournalHeaders", x => x.JournalId);
                });

            migrationBuilder.CreateTable(
                name: "ap_PaymentAllocations",
                columns: table => new
                {
                    AllocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_PaymentAllocations", x => x.AllocationId);
                    table.ForeignKey(
                        name: "FK_ap_PaymentAllocations_ap_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "ap_Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_PaymentAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_PaymentAttachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_ap_PaymentAttachments_ap_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "ap_Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_PaymentLines",
                columns: table => new
                {
                    PaymentLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineNo = table.Column<int>(type: "int", nullable: false),
                    ExpenseAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ap_PaymentLines", x => x.PaymentLineId);
                    table.ForeignKey(
                        name: "FK_ap_PaymentLines_ap_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "ap_Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ap_PaymentLines_gl_Accounts_ExpenseAccountId",
                        column: x => x.ExpenseAccountId,
                        principalTable: "gl_Accounts",
                        principalColumn: "GlAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fin_BankDepositAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepositId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fin_BankDepositAttachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_fin_BankDepositAttachments_fin_BankDeposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "fin_BankDeposits",
                        principalColumn: "DepositId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fin_BankDepositLines",
                columns: table => new
                {
                    DepositLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepositId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineNo = table.Column<int>(type: "int", nullable: false),
                    SourceAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fin_BankDepositLines", x => x.DepositLineId);
                    table.ForeignKey(
                        name: "FK_fin_BankDepositLines_fin_BankDeposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "fin_BankDeposits",
                        principalColumn: "DepositId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fin_BankDepositLines_gl_Accounts_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "gl_Accounts",
                        principalColumn: "GlAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gl_JournalAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_JournalAttachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_gl_JournalAttachments_gl_JournalHeaders_JournalId",
                        column: x => x.JournalId,
                        principalTable: "gl_JournalHeaders",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gl_JournalLines",
                columns: table => new
                {
                    JournalLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    LineNo = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_JournalLines", x => x.JournalLineId);
                    table.ForeignKey(
                        name: "FK_gl_JournalLines_gl_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "gl_Accounts",
                        principalColumn: "GlAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gl_JournalLines_gl_JournalHeaders_JournalId",
                        column: x => x.JournalId,
                        principalTable: "gl_JournalHeaders",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ap_PaymentAllocations_PaymentId",
                table: "ap_PaymentAllocations",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ap_PaymentAttachments_PaymentId",
                table: "ap_PaymentAttachments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ap_PaymentLines_ExpenseAccountId",
                table: "ap_PaymentLines",
                column: "ExpenseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ap_PaymentLines_PaymentId",
                table: "ap_PaymentLines",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ap_Payments_BusinessDate",
                table: "ap_Payments",
                columns: new[] { "BusinessId", "PaymentDate", "PaymentId" });

            migrationBuilder.CreateIndex(
                name: "IX_ap_Payments_MethodCheque",
                table: "ap_Payments",
                columns: new[] { "BusinessId", "PaymentMethod", "ChequeNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_fin_BankDepositAttachments_DepositId",
                table: "fin_BankDepositAttachments",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_fin_BankDepositLines_DepositId",
                table: "fin_BankDepositLines",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_fin_BankDepositLines_SourceAccountId",
                table: "fin_BankDepositLines",
                column: "SourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_fin_BankDeposits_BusinessDate",
                table: "fin_BankDeposits",
                columns: new[] { "BusinessId", "DepositDate", "DepositId" });

            migrationBuilder.CreateIndex(
                name: "IX_fin_BankDeposits_BusinessId_DepositNumber",
                table: "fin_BankDeposits",
                columns: new[] { "BusinessId", "DepositNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalAttachments_Journal",
                table: "gl_JournalAttachments",
                columns: new[] { "JournalId", "UploadedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalHeaders_BusinessDate",
                table: "gl_JournalHeaders",
                columns: new[] { "BusinessId", "JournalDate", "JournalId" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalHeaders_PostingDate",
                table: "gl_JournalHeaders",
                columns: new[] { "BusinessId", "PostingDate", "JournalId" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalHeaders_Source",
                table: "gl_JournalHeaders",
                columns: new[] { "BusinessId", "SourceType", "SourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalLines_Account",
                table: "gl_JournalLines",
                columns: new[] { "BusinessId", "AccountId" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalLines_AccountId",
                table: "gl_JournalLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalLines_Journal",
                table: "gl_JournalLines",
                columns: new[] { "JournalId", "LineNo" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_JournalLines_StoreAccount",
                table: "gl_JournalLines",
                columns: new[] { "BusinessId", "StoreId", "AccountId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ap_PaymentAllocations");

            migrationBuilder.DropTable(
                name: "ap_PaymentAttachments");

            migrationBuilder.DropTable(
                name: "ap_PaymentLines");

            migrationBuilder.DropTable(
                name: "fin_BankDepositAttachments");

            migrationBuilder.DropTable(
                name: "fin_BankDepositLines");

            migrationBuilder.DropTable(
                name: "gl_BusinessSettings");

            migrationBuilder.DropTable(
                name: "gl_JournalAttachments");

            migrationBuilder.DropTable(
                name: "gl_JournalLines");

            migrationBuilder.DropTable(
                name: "ap_Payments");

            migrationBuilder.DropTable(
                name: "fin_BankDeposits");

            migrationBuilder.DropTable(
                name: "gl_JournalHeaders");
        }
    }
}
