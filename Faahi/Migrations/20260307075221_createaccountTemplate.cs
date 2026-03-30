using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class createaccountTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gl_AccountTemps",
                columns: table => new
                {
                    GlAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    BalanceType = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    NormalBalance = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    DetailType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPostable = table.Column<string>(type: "Char(1)", nullable: false),
                    IsActive = table.Column<string>(type: "Char(1)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AsOfDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gl_AccountTemps");
        }
    }
}
