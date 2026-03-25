using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class accountmappingTWO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gl_AccountMapping",
                columns: table => new
                {
                    DefaultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    PurposeCode = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    GlAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_AccountMapping", x => x.DefaultId);
                    table.ForeignKey(
                        name: "FK_gl_AccountMapping_co_business_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gl_AccountMapping_gl_Accounts_GlAccountId",
                        column: x => x.GlAccountId,
                        principalTable: "gl_Accounts",
                        principalColumn: "GlAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_gl_AccountMapping_st_stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_gl_AccountMapping_CompanyId",
                table: "gl_AccountMapping",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_gl_AccountMapping_GlAccountId",
                table: "gl_AccountMapping",
                column: "GlAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_gl_AccountMapping_StoreId",
                table: "gl_AccountMapping",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gl_AccountMapping");
        }
    }
}
