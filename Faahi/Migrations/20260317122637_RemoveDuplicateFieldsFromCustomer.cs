using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuplicateFieldsFromCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "default_currency",
                table: "ar_Customers");

            migrationBuilder.DropColumn(
                name: "tex_identification_number",
                table: "ar_Customers");

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
                name: "default_currency",
                table: "ap_Vendors");

            migrationBuilder.DropColumn(
                name: "tex_identification_number",
                table: "ap_Vendors");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "ar_Customers",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "ap_Vendors",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "idx_party_id",
                table: "ar_Customers",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "idx_status",
                table: "ar_Customers",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_party_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "idx_status",
                table: "ar_Customers");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "ar_Customers",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

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
                name: "default_currency",
                table: "ar_Customers",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tex_identification_number",
                table: "ar_Customers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "ap_Vendors",
                type: "varchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

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
                name: "default_currency",
                table: "ap_Vendors",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tex_identification_number",
                table: "ap_Vendors",
                type: "nvarchar(50)",
                nullable: true);
        }
    }
}
