using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class allglaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gl_JournalLines_gl_Accounts_AccountId",
                table: "gl_JournalLines");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_co_business_CompanyId",
                table: "gl_ledger");

            migrationBuilder.DropIndex(
                name: "IX_gl_Ledger_ModuleDate",
                table: "gl_ledger");

            migrationBuilder.DropIndex(
                name: "IX_gl_Ledger_Source",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "Debit",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "IsReversed",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "ReversalOfId",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "gl_ledger");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "gl_ledger",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_gl_Ledger_CompanyTransactionDate",
                table: "gl_ledger",
                newName: "IX_gl_Ledger_BusinessTransactionDate");

            migrationBuilder.RenameIndex(
                name: "IX_gl_Ledger_CompanyDateAccount",
                table: "gl_ledger",
                newName: "IX_gl_Ledger_BusinessDateAccount");

            migrationBuilder.RenameColumn(
                name: "DebitAmount",
                table: "gl_JournalLines",
                newName: "DebitAmountFC");

            migrationBuilder.RenameColumn(
                name: "CreditAmount",
                table: "gl_JournalLines",
                newName: "DebitAmountBC");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "gl_JournalLines",
                newName: "GlAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_gl_JournalLines_AccountId",
                table: "gl_JournalLines",
                newName: "IX_gl_JournalLines_GlAccountId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "gl_ledger",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<Guid>(
                name: "SourceId",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostingDate",
                table: "gl_ledger",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeRate",
                table: "gl_ledger",
                type: "decimal(18,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "gl_ledger",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaseCurrencyCode",
                table: "gl_ledger",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmountBC",
                table: "gl_ledger",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmountFC",
                table: "gl_ledger",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DebitAmountBC",
                table: "gl_ledger",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DebitAmountFC",
                table: "gl_ledger",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SourceType",
                table: "gl_ledger",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionCurrencyCode",
                table: "gl_ledger",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CostCenterId",
                table: "gl_JournalLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "gl_JournalLines",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmountBC",
                table: "gl_JournalLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmountFC",
                table: "gl_JournalLines",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "gl_JournalLines",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "gl_JournalLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "gl_JournalLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "gl_JournalLines",
                type: "decimal(18,8)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceLineId",
                table: "gl_JournalLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceType",
                table: "gl_JournalLines",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "gl_JournalLines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "gl_JournalLines",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "gl_JournalLines",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaseCurrencyCode",
                table: "gl_JournalHeaders",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "gl_JournalHeaders",
                type: "decimal(18,8)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemGenerated",
                table: "gl_JournalHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedAt",
                table: "gl_JournalHeaders",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostedBy",
                table: "gl_JournalHeaders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNo",
                table: "gl_JournalHeaders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "gl_JournalHeaders",
                type: "nvarchar(500)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReversalOfJournalId",
                table: "gl_JournalHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "gl_JournalHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCreditBC",
                table: "gl_JournalHeaders",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCreditFC",
                table: "gl_JournalHeaders",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDebitBC",
                table: "gl_JournalHeaders",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDebitFC",
                table: "gl_JournalHeaders",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionCurrencyCode",
                table: "gl_JournalHeaders",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "gl_JournalHeaders",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "gl_JournalHeaders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_Source",
                table: "gl_ledger",
                columns: new[] { "SourceType", "SourceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_gl_JournalLines_gl_Accounts_GlAccountId",
                table: "gl_JournalLines",
                column: "GlAccountId",
                principalTable: "gl_Accounts",
                principalColumn: "GlAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_co_business_BusinessId",
                table: "gl_ledger",
                column: "BusinessId",
                principalTable: "co_business",
                principalColumn: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gl_JournalLines_gl_Accounts_GlAccountId",
                table: "gl_JournalLines");

            migrationBuilder.DropForeignKey(
                name: "FK_gl_ledger_co_business_BusinessId",
                table: "gl_ledger");

            migrationBuilder.DropIndex(
                name: "IX_gl_Ledger_Source",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "BaseCurrencyCode",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "CreditAmountBC",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "CreditAmountFC",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "DebitAmountBC",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "DebitAmountFC",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "TransactionCurrencyCode",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "CreditAmountBC",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "CreditAmountFC",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "SourceLineId",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "gl_JournalLines");

            migrationBuilder.DropColumn(
                name: "BaseCurrencyCode",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "IsSystemGenerated",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "PostedAt",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "PostedBy",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "ReferenceNo",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "ReversalOfJournalId",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "TotalCreditBC",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "TotalCreditFC",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "TotalDebitBC",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "TotalDebitFC",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "TransactionCurrencyCode",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "gl_JournalHeaders");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "gl_JournalHeaders");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "gl_ledger",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_gl_Ledger_BusinessTransactionDate",
                table: "gl_ledger",
                newName: "IX_gl_Ledger_CompanyTransactionDate");

            migrationBuilder.RenameIndex(
                name: "IX_gl_Ledger_BusinessDateAccount",
                table: "gl_ledger",
                newName: "IX_gl_Ledger_CompanyDateAccount");

            migrationBuilder.RenameColumn(
                name: "GlAccountId",
                table: "gl_JournalLines",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "DebitAmountFC",
                table: "gl_JournalLines",
                newName: "DebitAmount");

            migrationBuilder.RenameColumn(
                name: "DebitAmountBC",
                table: "gl_JournalLines",
                newName: "CreditAmount");

            migrationBuilder.RenameIndex(
                name: "IX_gl_JournalLines_GlAccountId",
                table: "gl_JournalLines",
                newName: "IX_gl_JournalLines_AccountId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "gl_ledger",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<Guid>(
                name: "SourceId",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostingDate",
                table: "gl_ledger",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeRate",
                table: "gl_ledger",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "gl_ledger",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Debit",
                table: "gl_ledger",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsReversed",
                table: "gl_ledger",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ReversalOfId",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_ModuleDate",
                table: "gl_ledger",
                columns: new[] { "Module", "PostingDate" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_Source",
                table: "gl_ledger",
                columns: new[] { "Module", "SourceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_gl_JournalLines_gl_Accounts_AccountId",
                table: "gl_JournalLines",
                column: "AccountId",
                principalTable: "gl_Accounts",
                principalColumn: "GlAccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gl_ledger_co_business_CompanyId",
                table: "gl_ledger",
                column: "CompanyId",
                principalTable: "co_business",
                principalColumn: "company_id");
        }
    }
}
