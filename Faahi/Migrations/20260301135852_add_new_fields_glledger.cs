using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class add_new_fields_glledger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "gl_ledger",
                type: "decimal(18,6)",
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
                name: "SourceId",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SourceLineId",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "gl_ledger",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "gl_ledger",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "gl_ledger",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_CompanyTransactionDate",
                table: "gl_ledger",
                columns: new[] { "CompanyId", "TransactionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_Ledger_Source",
                table: "gl_ledger",
                columns: new[] { "Module", "SourceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_gl_Ledger_CompanyTransactionDate",
                table: "gl_ledger");

            migrationBuilder.DropIndex(
                name: "IX_gl_Ledger_Source",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "IsReversed",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "ReversalOfId",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "SourceLineId",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "gl_ledger");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "gl_ledger");

        }
    }
}
