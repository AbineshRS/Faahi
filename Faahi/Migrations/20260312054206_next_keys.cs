using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class next_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key");

            migrationBuilder.AddColumn<Guid>(
                name: "business_id",
                table: "am_table_next_key",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");
            migrationBuilder.AddPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key",
                column: "business_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key");

            migrationBuilder.DropColumn(
                name: "business_id",
                table: "am_table_next_key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key",
                column: "name");
        }
    }
}
