using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class _pos_re : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pos_ReturnsalePayments",
                columns: table => new
                {
                    pos_retuen_sales_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sale_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_method_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    terminal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    shift_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    drawer_session_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    receipt_no = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    line_no = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    currency_code = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    fx_rate = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    base_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    change_given = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    reference_no = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    is_voided = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "F"),
                    voided_by = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    voided_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pos_ReturnsalePayments", x => x.pos_retuen_sales_id);
                    table.ForeignKey(
                        name: "FK_pos_ReturnsalePayments_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_pos_ReturnsalePayments_so_Payment_Types_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "so_Payment_Types",
                        principalColumn: "payment_type_id");
                    table.ForeignKey(
                        name: "FK_pos_ReturnsalePayments_so_SalesHeaders_sale_id",
                        column: x => x.sale_id,
                        principalTable: "so_SalesHeaders",
                        principalColumn: "sales_id");
                    table.ForeignKey(
                        name: "FK_pos_ReturnsalePayments_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_business_id",
                table: "pos_ReturnsalePayments",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_created_at",
                table: "pos_ReturnsalePayments",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_line_no",
                table: "pos_ReturnsalePayments",
                column: "line_no");

            migrationBuilder.CreateIndex(
                name: "IX_payment_method_id",
                table: "pos_ReturnsalePayments",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_pos_retuen_sales_id",
                table: "pos_ReturnsalePayments",
                column: "pos_retuen_sales_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sale_id",
                table: "pos_ReturnsalePayments",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_store_id",
                table: "pos_ReturnsalePayments",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_terminal_id",
                table: "pos_ReturnsalePayments",
                column: "terminal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pos_ReturnsalePayments");
        }
    }
}
