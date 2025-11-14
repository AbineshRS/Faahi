using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class ar_cust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contact_email",
                table: "ar_Customers",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_name",
                table: "ar_Customers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_phone1",
                table: "ar_Customers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_phone2",
                table: "ar_Customers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tex_identification_number",
                table: "ar_Customers",
                type: "nvarchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contact_email",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "contact_name",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "contact_phone1",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "contact_phone2",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "tex_identification_number",
                table: "ar_Customers");
        }
    }
}
