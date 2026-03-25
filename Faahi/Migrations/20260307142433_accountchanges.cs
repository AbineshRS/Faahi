using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class accountchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gl_AccountTemps");

            migrationBuilder.AlterColumn<string>(
                name: "BalanceType",
                table: "gl_Accounts",
                type: "nvarchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BalanceType",
                table: "gl_Accounts",
                type: "nvarchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "gl_AccountTemps",
                columns: table => new
                {
                    GlAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    AsOfDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BalanceType = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    DetailType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsActive = table.Column<string>(type: "Char(1)", nullable: false),
                    IsPostable = table.Column<string>(type: "Char(1)", nullable: false),
                    NormalBalance = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_AccountTemps", x => x.GlAccountId);
                    table.ForeignKey(
                        name: "FK_gl_AccountTemps_co_business_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gl_AccountTemps_gl_Accounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "gl_Accounts",
                        principalColumn: "GlAccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_gl_Accounts_BusinessType",
                table: "gl_AccountTemps",
                columns: new[] { "CompanyId", "AccountType", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_gl_AccountTemps_ParentAccountId",
                table: "gl_AccountTemps",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "UQ_gl_Accounts_BusinessCode",
                table: "gl_AccountTemps",
                column: "AccountName");
        }
    }
}
