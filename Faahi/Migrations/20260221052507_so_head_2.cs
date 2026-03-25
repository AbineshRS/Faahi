using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class so_head_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "st_Invoice_Templates",
                columns: table => new
                {
                    invoices_temp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    invoices_temp_name = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    invoices_temp_description = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Invoice_Templates", x => x.invoices_temp_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "st_Invoice_Templates");
        }
    }
}
