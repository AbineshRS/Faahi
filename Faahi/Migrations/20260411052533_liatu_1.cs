using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class liatu_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrders_mk_customer_profiles_customer_profile_id",
                table: "om_CustomerOrders");

            migrationBuilder.RenameColumn(
                name: "customer_profile_id",
                table: "om_CustomerOrders",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_om_CustomerOrders_customer_profile_id",
                table: "om_CustomerOrders",
                newName: "IX_om_CustomerOrders_customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrders_ar_Customers_customer_id",
                table: "om_CustomerOrders",
                column: "customer_id",
                principalTable: "ar_Customers",
                principalColumn: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_om_CustomerOrders_ar_Customers_customer_id",
                table: "om_CustomerOrders");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "om_CustomerOrders",
                newName: "customer_profile_id");

            migrationBuilder.RenameIndex(
                name: "IX_om_CustomerOrders_customer_id",
                table: "om_CustomerOrders",
                newName: "IX_om_CustomerOrders_customer_profile_id");

            migrationBuilder.AddForeignKey(
                name: "FK_om_CustomerOrders_mk_customer_profiles_customer_profile_id",
                table: "om_CustomerOrders",
                column: "customer_profile_id",
                principalTable: "mk_customer_profiles",
                principalColumn: "customer_profile_id");
        }
    }
}
