using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class update_indexs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_st_Users_account_type",
                table: "st_Users",
                column: "account_type");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_company_id",
                table: "st_Users",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_email",
                table: "st_Users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_Full_name",
                table: "st_Users",
                column: "Full_name");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_phone",
                table: "st_Users",
                column: "phone");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_registration_date",
                table: "st_Users",
                column: "registration_date");

            migrationBuilder.CreateIndex(
                name: "IX_st_Users_status",
                table: "st_Users",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_company_id",
                table: "st_stores",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_created_at",
                table: "st_stores",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_status",
                table: "st_stores",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_store_name",
                table: "st_stores",
                column: "store_name");

            migrationBuilder.CreateIndex(
                name: "IX_st_stores_store_type",
                table: "st_stores",
                column: "store_type");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_business_name",
                table: "co_business",
                column: "business_name");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_country",
                table: "co_business",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_created_at",
                table: "co_business",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_createdSites",
                table: "co_business",
                column: "createdSites");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_createdSites_users",
                table: "co_business",
                column: "createdSites_users");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_edit_date_time",
                table: "co_business",
                column: "edit_date_time");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_edit_user_id",
                table: "co_business",
                column: "edit_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_email",
                table: "co_business",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_phoneNumber",
                table: "co_business",
                column: "phoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_plan_type",
                table: "co_business",
                column: "plan_type");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_reg_no",
                table: "co_business",
                column: "reg_no");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_sites_allowed",
                table: "co_business",
                column: "sites_allowed");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_sites_users_allowed",
                table: "co_business",
                column: "sites_users_allowed");

            migrationBuilder.CreateIndex(
                name: "IX_co_business_tin_number",
                table: "co_business",
                column: "tin_number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_st_Users_account_type",
                table: "st_Users");

            migrationBuilder.DropIndex(
                name: "IX_st_Users_company_id",
                table: "st_Users");

            migrationBuilder.DropIndex(
                name: "IX_st_Users_email",
                table: "st_Users");

            migrationBuilder.DropIndex(
                name: "IX_st_Users_Full_name",
                table: "st_Users");

            migrationBuilder.DropIndex(
                name: "IX_st_Users_phone",
                table: "st_Users");

            migrationBuilder.DropIndex(
                name: "IX_st_Users_registration_date",
                table: "st_Users");

            migrationBuilder.DropIndex(
                name: "IX_st_Users_status",
                table: "st_Users");

            migrationBuilder.DropIndex(
                name: "IX_st_stores_company_id",
                table: "st_stores");

            migrationBuilder.DropIndex(
                name: "IX_st_stores_created_at",
                table: "st_stores");

            migrationBuilder.DropIndex(
                name: "IX_st_stores_status",
                table: "st_stores");

            migrationBuilder.DropIndex(
                name: "IX_st_stores_store_name",
                table: "st_stores");

            migrationBuilder.DropIndex(
                name: "IX_st_stores_store_type",
                table: "st_stores");

            migrationBuilder.DropIndex(
                name: "IX_co_business_business_name",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_country",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_created_at",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_createdSites",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_createdSites_users",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_edit_date_time",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_edit_user_id",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_email",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_phoneNumber",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_plan_type",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_reg_no",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_sites_allowed",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_sites_users_allowed",
                table: "co_business");

            migrationBuilder.DropIndex(
                name: "IX_co_business_tin_number",
                table: "co_business");
        }
    }
}
