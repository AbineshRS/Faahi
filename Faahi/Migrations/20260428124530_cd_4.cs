using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class cd_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ar_Customers_ar_customer_due_payment_term_id",
                table: "ar_Customers");

            migrationBuilder.DropTable(
                name: "ar_customer_due");

            migrationBuilder.CreateTable(
                name: "payment_terms",
                columns: table => new
                {
                    payment_term_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    due_days = table.Column<int>(type: "int", nullable: false),
                    due_description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "T")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_terms", x => x.payment_term_id);
                    table.ForeignKey(
                        name: "FK_payment_terms_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ar_customer_due",
                table: "payment_terms",
                columns: new[] { "payment_term_id", "business_id" });

            migrationBuilder.CreateIndex(
                name: "IX_created_at",
                table: "payment_terms",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_payment_terms_business_id",
                table: "payment_terms",
                column: "business_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ar_Customers_payment_terms_payment_term_id",
                table: "ar_Customers",
                column: "payment_term_id",
                principalTable: "payment_terms",
                principalColumn: "payment_term_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ar_Customers_payment_terms_payment_term_id",
                table: "ar_Customers");

            migrationBuilder.DropTable(
                name: "payment_terms");

            migrationBuilder.CreateTable(
                name: "ar_customer_due",
                columns: table => new
                {
                    payment_term_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    due_days = table.Column<int>(type: "int", nullable: false),
                    due_description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ar_customer_due", x => x.payment_term_id);
                    table.ForeignKey(
                        name: "FK_ar_customer_due_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ar_customer_due_business_id",
                table: "ar_customer_due",
                column: "business_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ar_Customers_ar_customer_due_payment_term_id",
                table: "ar_Customers",
                column: "payment_term_id",
                principalTable: "ar_customer_due",
                principalColumn: "payment_term_id");
        }
    }
}
