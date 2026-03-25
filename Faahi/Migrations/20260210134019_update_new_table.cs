using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class update_new_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AccountParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_AccountTypes_AccountTypes_AccountParentId",
                        column: x => x.AccountParentId,
                        principalTable: "AccountTypes",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_AccountId",
                table: "AccountTypes",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_AccountParentId",
                table: "AccountTypes",
                column: "AccountParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTypes");
        }
    }
}
