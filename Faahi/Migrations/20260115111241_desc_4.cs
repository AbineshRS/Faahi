using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class desc_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "party_type",
                table: "st_Parties",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.CreateIndex(
                name: "idx_address_id",
                table: "st_PartyAddresses",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_customer_id",
                table: "st_PartyAddresses",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_party_id",
                table: "st_PartyAddresses",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "idx_vendor_id",
                table: "st_PartyAddresses",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "idx_company_id",
                table: "st_Parties",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "idx_email",
                table: "st_Parties",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "idx_party_id",
                table: "st_Parties",
                column: "party_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_status",
                table: "st_Parties",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_company_id",
                table: "ar_Customers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "idx_customer_code",
                table: "ar_Customers",
                column: "customer_code");

            migrationBuilder.CreateIndex(
                name: "idx_customer_id",
                table: "ar_Customers",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_price_tier_id",
                table: "ar_Customers",
                column: "price_tier_id");

            migrationBuilder.CreateIndex(
                name: "idx_company_id",
                table: "ap_Vendors",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "idx_party_id",
                table: "ap_Vendors",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "idx_status",
                table: "ap_Vendors",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "idx_vendor_code",
                table: "ap_Vendors",
                column: "vendor_code");

            migrationBuilder.CreateIndex(
                name: "idx_vendor_id",
                table: "ap_Vendors",
                column: "vendor_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_address_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropIndex(
                name: "idx_customer_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropIndex(
                name: "idx_party_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropIndex(
                name: "idx_vendor_id",
                table: "st_PartyAddresses");

            migrationBuilder.DropIndex(
                name: "idx_company_id",
                table: "st_Parties");

            migrationBuilder.DropIndex(
                name: "idx_email",
                table: "st_Parties");

            migrationBuilder.DropIndex(
                name: "idx_party_id",
                table: "st_Parties");

            migrationBuilder.DropIndex(
                name: "idx_status",
                table: "st_Parties");

            migrationBuilder.DropIndex(
                name: "idx_company_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "idx_customer_code",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "idx_customer_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "idx_price_tier_id",
                table: "ar_Customers");

            migrationBuilder.DropIndex(
                name: "idx_company_id",
                table: "ap_Vendors");

            migrationBuilder.DropIndex(
                name: "idx_party_id",
                table: "ap_Vendors");

            migrationBuilder.DropIndex(
                name: "idx_status",
                table: "ap_Vendors");

            migrationBuilder.DropIndex(
                name: "idx_vendor_code",
                table: "ap_Vendors");

            migrationBuilder.DropIndex(
                name: "idx_vendor_id",
                table: "ap_Vendors");

            migrationBuilder.AlterColumn<string>(
                name: "party_type",
                table: "st_Parties",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);
        }
    }
}
