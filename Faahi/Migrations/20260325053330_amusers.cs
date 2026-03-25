using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class amusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mk_customer_profiles",
                columns: table => new
                {
                    customer_profile_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_code = table.Column<string>(type: "varchar(32)", nullable: true),
                    gender = table.Column<string>(type: "varchar(20)", nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    preferred_language = table.Column<string>(type: "varchar(10)", nullable: true),
                    notes = table.Column<string>(type: "varchar(300)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    am_user_rolesuser_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mk_customer_profiles", x => x.customer_profile_id);
                    table.ForeignKey(
                        name: "FK_mk_customer_profiles_am_user_roles_am_user_rolesuser_role_id",
                        column: x => x.am_user_rolesuser_role_id,
                        principalTable: "am_user_roles",
                        principalColumn: "user_role_id");
                    table.ForeignKey(
                        name: "FK_mk_customer_profiles_am_users_user_id",
                        column: x => x.user_id,
                        principalTable: "am_users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mk_customer_addresses",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address_type = table.Column<string>(type: "varchar(20)", nullable: true),
                    contact_name = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    address_line1 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    address_line2 = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    state_region = table.Column<string>(type: "varchar(20)", nullable: true),
                    postal_code = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    country_code = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    is_default = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    mk_customer_profilescustomer_profile_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mk_customer_addresses", x => x.address_id);
                    table.ForeignKey(
                        name: "FK_mk_customer_addresses_am_users_user_id",
                        column: x => x.user_id,
                        principalTable: "am_users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mk_customer_addresses_mk_customer_profiles_mk_customer_profilescustomer_profile_id",
                        column: x => x.mk_customer_profilescustomer_profile_id,
                        principalTable: "mk_customer_profiles",
                        principalColumn: "customer_profile_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_id",
                table: "mk_customer_addresses",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_mk_customer_profilescustomer_profile_id",
                table: "mk_customer_addresses",
                column: "mk_customer_profilescustomer_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_addresses_user_id",
                table: "mk_customer_addresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_id",
                table: "mk_customer_profiles",
                column: "customer_profile_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_profiles_am_user_rolesuser_role_id",
                table: "mk_customer_profiles",
                column: "am_user_rolesuser_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_mk_customer_profiles_user_id",
                table: "mk_customer_profiles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mk_customer_addresses");

            migrationBuilder.DropTable(
                name: "mk_customer_profiles");
        }
    }
}
