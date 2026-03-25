using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class deatilscodes_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "am_roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_code = table.Column<string>(type: "varchar(50)", nullable: false),
                    role_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    role_group = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(300)", nullable: true),
                    is_system_role = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    am_usersuserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_am_roles", x => x.role_id);
                    table.ForeignKey(
                        name: "FK_am_roles_am_users_am_usersuserId",
                        column: x => x.am_usersuserId,
                        principalTable: "am_users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "am_user_roles",
                columns: table => new
                {
                    user_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    am_rolesrole_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_am_user_roles", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_am_user_roles_am_roles_am_rolesrole_id",
                        column: x => x.am_rolesrole_id,
                        principalTable: "am_roles",
                        principalColumn: "role_id");
                    table.ForeignKey(
                        name: "FK_am_user_roles_am_users_user_id",
                        column: x => x.user_id,
                        principalTable: "am_users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_am_user_roles_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_am_user_roles_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateTable(
                name: "am_user_business_access",
                columns: table => new
                {
                    access_id = table.Column<Guid>(type: "uniqueidentifier ", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    access_level = table.Column<string>(type: "varchar(30)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    am_user_rolesuser_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_am_user_business_access", x => x.access_id);
                    table.ForeignKey(
                        name: "FK_am_user_business_access_am_user_roles_am_user_rolesuser_role_id",
                        column: x => x.am_user_rolesuser_role_id,
                        principalTable: "am_user_roles",
                        principalColumn: "user_role_id");
                    table.ForeignKey(
                        name: "FK_am_user_business_access_am_users_user_id",
                        column: x => x.user_id,
                        principalTable: "am_users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_am_user_business_access_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_am_user_business_access_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "UX_am_users_email",
                table: "am_users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "UX_am_users_user_name",
                table: "am_users",
                column: "userName");

            migrationBuilder.CreateIndex(
                name: "IX_am_roles_am_usersuserId",
                table: "am_roles",
                column: "am_usersuserId");

            migrationBuilder.CreateIndex(
                name: "IX_am_user_business_access_am_user_rolesuser_role_id",
                table: "am_user_business_access",
                column: "am_user_rolesuser_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_am_user_business_access_store_id",
                table: "am_user_business_access",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_am_user_business_access_user_business_store",
                table: "am_user_business_access",
                columns: new[] { "user_id", "business_id", "store_id" });

            migrationBuilder.CreateIndex(
                name: "X_am_user_business_access_business_store",
                table: "am_user_business_access",
                columns: new[] { "business_id", "store_id" });

            migrationBuilder.CreateIndex(
                name: "IX_am_user_roles_am_rolesrole_id",
                table: "am_user_roles",
                column: "am_rolesrole_id");

            migrationBuilder.CreateIndex(
                name: "IX_am_user_roles_business_store",
                table: "am_user_roles",
                columns: new[] { "business_id", "store_id" });

            migrationBuilder.CreateIndex(
                name: "IX_am_user_roles_store_id",
                table: "am_user_roles",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_am_user_roles_user_id",
                table: "am_user_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "am_user_business_access");

            migrationBuilder.DropTable(
                name: "am_user_roles");

            migrationBuilder.DropTable(
                name: "am_roles");

            migrationBuilder.DropIndex(
                name: "UX_am_users_email",
                table: "am_users");

            migrationBuilder.DropIndex(
                name: "UX_am_users_user_name",
                table: "am_users");
        }
    }
}
