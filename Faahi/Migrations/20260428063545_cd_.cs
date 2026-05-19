using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class cd_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ar_customer_due",
                columns: table => new
                {
                    customer_due_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    due_description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    status = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "T")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ar_customer_due", x => x.customer_due_id);
                    table.ForeignKey(
                        name: "FK_ar_customer_due_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ar_customer_due",
                table: "ar_customer_due",
                columns: new[] { "customer_due_id", "business_id" });

            migrationBuilder.CreateIndex(
                name: "IX_ar_customer_due_business_id",
                table: "ar_customer_due",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_created_at",
                table: "ar_customer_due",
                column: "created_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ar_customer_due");
        }
    }
}
