using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class sa_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sa_roles",
                columns: table => new
                {
                    sa_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sa_role_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    sa_description = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    sa_status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sa_roles", x => x.sa_role_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sa_roles");
        }
    }
}
