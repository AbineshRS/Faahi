using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class ar_cust_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contact_email",
                table: "ap_Vendors",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_name",
                table: "ap_Vendors",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_phone1",
                table: "ap_Vendors",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_phone2",
                table: "ap_Vendors",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_website",
                table: "ap_Vendors",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tex_identification_number",
                table: "ap_Vendors",
                type: "nvarchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contact_email",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "contact_name",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "contact_phone1",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "contact_phone2",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "contact_website",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "tex_identification_number",
                table: "ap_Vendors");
        }
    }
}
