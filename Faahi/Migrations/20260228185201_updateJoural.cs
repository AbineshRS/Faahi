using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class updateJoural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_gl_AccountMapping_CompanyId",
                table: "gl_AccountMapping");

            migrationBuilder.AlterColumn<Guid>(
                name: "GlAccountId",
                table: "gl_AccountMapping",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "gl_ledger",
                columns: table => new
                {
                    LedgerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JournalLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "date", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_ledger", x => x.LedgerId);
                    table.ForeignKey(
                        name: "FK_gl_ledger_co_business_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_gl_ledger_gl_Accounts_GlAccountId",
                        column: x => x.GlAccountId,
                        principalTable: "gl_Accounts",
                        principalColumn: "GlAccountId");
                    table.ForeignKey(
                        name: "FK_gl_ledger_gl_JournalHeaders_JournalId",
                        column: x => x.JournalId,
                        principalTable: "gl_JournalHeaders",
                        principalColumn: "JournalId");
                    table.ForeignKey(
                        name: "FK_gl_ledger_gl_JournalLines_JournalLineId",
                        column: x => x.JournalLineId,
                        principalTable: "gl_JournalLines",
                        principalColumn: "JournalLineId");
                });

            migrationBuilder.CreateIndex(
                name: "UQ_gl_AccountDefaults_CompanyPurpose",
                table: "gl_AccountMapping",
                columns: new[] { "CompanyId", "StoreId", "Module", "PurposeCode" },
                unique: true,
                filter: "[StoreId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_CompanyDateAccount",
                table: "gl_ledger",
                columns: new[] { "CompanyId", "PostingDate", "GlAccountId" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_ledger_GlAccountId",
                table: "gl_ledger",
                column: "GlAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_gl_ledger_JournalLineId",
                table: "gl_ledger",
                column: "JournalLineId");

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_JournalRefs",
                table: "gl_ledger",
                columns: new[] { "JournalId", "JournalLineId" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_ModuleDate",
                table: "gl_ledger",
                columns: new[] { "Module", "PostingDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gl_ledger");

            migrationBuilder.DropIndex(
                name: "UQ_gl_AccountDefaults_CompanyPurpose",
                table: "gl_AccountMapping");

            migrationBuilder.AlterColumn<Guid>(
                name: "GlAccountId",
                table: "gl_AccountMapping",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_gl_AccountMapping_CompanyId",
                table: "gl_AccountMapping",
                column: "CompanyId");
        }
    }
}
