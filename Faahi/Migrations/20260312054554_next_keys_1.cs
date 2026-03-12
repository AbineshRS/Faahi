using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class next_keys_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_am_table_next_key",
                table: "am_table_next_key",
                column: "business_id");
        }
    }
}
