using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class st_store_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "st_stores",
                columns: table => new
                {
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    store_location = table.Column<string>(type: "varchar(max)", nullable: true),
                    store_type = table.Column<string>(type: "varchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_stores", x => x.store_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "st_stores");
        }
    }
}
