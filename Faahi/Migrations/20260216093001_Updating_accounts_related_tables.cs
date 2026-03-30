using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class Updating_accounts_related_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "AccountTypes",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.CreateTable(
                name: "gl_Accounts",
                columns: table => new
                {
                    GlAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AccountType = table.Column<string>(type: "varchar(20)", nullable: false),
                    DetailType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPostable = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "T"),
                    IsActive = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "T"),
                    CurrencyCode = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AsOfDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_Accounts", x => x.GlAccountId);
                    table.ForeignKey(
                        name: "FK_gl_Accounts_co_business_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gl_Accounts_gl_Accounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "gl_Accounts",
                        principalColumn: "GlAccountId");
                });

            migrationBuilder.CreateTable(
                name: "gl_FiscalPeriods",
                columns: table => new
                {
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiscalYear = table.Column<int>(type: "int", nullable: false),
                    FiscalMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsClosed = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "F"),
                    ClosedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClosedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_FiscalPeriods", x => x.PeriodId);
                    table.ForeignKey(
                        name: "FK_gl_FiscalPeriods_co_business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gl_Accounts_BusinessType",
                table: "gl_Accounts",
                columns: new[] { "CompanyId", "AccountType", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_Accounts_ParentAccountId",
                table: "gl_Accounts",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "UQ_gl_Accounts_BusinessCode",
                table: "gl_Accounts",
                columns: new[] { "CompanyId", "AccountName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gl_FiscalPeriods_BusinessClosed",
                table: "gl_FiscalPeriods",
                columns: new[] { "BusinessId", "IsClosed", "FiscalYear", "FiscalMonth" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gl_Accounts");

            migrationBuilder.DropTable(
                name: "gl_FiscalPeriods");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "AccountTypes",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);
        }
    }
}
