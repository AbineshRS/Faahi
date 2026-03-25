using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class next_keys_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key");

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "am_table_next_key",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "next_key_id",
                table: "am_table_next_key",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");
            migrationBuilder.AddPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key",
                column: "next_key_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key");

            migrationBuilder.DropColumn(
                name: "next_key_id",
                table: "am_table_next_key");

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "am_table_next_key",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key",
                column: "name");
        }
    }
}
