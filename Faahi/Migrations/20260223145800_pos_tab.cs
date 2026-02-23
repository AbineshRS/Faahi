using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class pos_tab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pos_DrawerSessions",
                columns: table => new
                {
                    drawer_session_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    terminal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    opened_by = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    closed_by = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    opening_float = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    expected_closing = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    actual_closing = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    difference_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    opened_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    status = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "O")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pos_DrawerSessions", x => x.drawer_session_id);
                    table.ForeignKey(
                        name: "FK_pos_DrawerSessions_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pos_DrawerSessions_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateTable(
                name: "pos_SalePayments",
                columns: table => new
                {
                    sale_payment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sale_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_method_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_pos_SalePayments", x => x.sale_payment_id);
                    table.ForeignKey(
                        name: "FK_pos_SalePayments_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pos_SalePayments_so_SalesHeaders_sale_id",
                        column: x => x.sale_id,
                        principalTable: "so_SalesHeaders",
                        principalColumn: "sales_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pos_SalePayments_st_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "st_stores",
                        principalColumn: "store_id");
                });

            migrationBuilder.CreateTable(
                name: "pos_DrawerCountDetails",
                columns: table => new
                {
                    drawer_count_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    drawer_session_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_method_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expected_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    counted_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    difference_amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    created_by = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pos_DrawerCountDetails", x => x.drawer_count_id);
                    table.ForeignKey(
                        name: "FK_pos_DrawerCountDetails_co_business_business_id",
                        column: x => x.business_id,
                        principalTable: "co_business",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pos_DrawerCountDetails_pos_DrawerSessions_drawer_session_id",
                        column: x => x.drawer_session_id,
                        principalTable: "pos_DrawerSessions",
                        principalColumn: "drawer_session_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_drawer_session_id",
                table: "pos_DrawerCountDetails",
                column: "drawer_session_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_method_id",
                table: "pos_DrawerCountDetails",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_pos_DrawerCountDetails_business_id",
                table: "pos_DrawerCountDetails",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_id",
                table: "pos_DrawerSessions",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_drawer_session_id",
                table: "pos_DrawerSessions",
                column: "drawer_session_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_opened_at",
                table: "pos_DrawerSessions",
                column: "opened_at");

            migrationBuilder.CreateIndex(
                name: "IX_store_id",
                table: "pos_DrawerSessions",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_terminal_id",
                table: "pos_DrawerSessions",
                column: "terminal_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_id",
                table: "pos_SalePayments",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_created_at",
                table: "pos_SalePayments",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_line_no",
                table: "pos_SalePayments",
                column: "line_no");

            migrationBuilder.CreateIndex(
                name: "IX_payment_method_id",
                table: "pos_SalePayments",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_id",
                table: "pos_SalePayments",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_sale_payment_id",
                table: "pos_SalePayments",
                column: "sale_payment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_store_id",
                table: "pos_SalePayments",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_terminal_id",
                table: "pos_SalePayments",
                column: "terminal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pos_DrawerCountDetails");

            migrationBuilder.DropTable(
                name: "pos_SalePayments");

            migrationBuilder.DropTable(
                name: "pos_DrawerSessions");
        }
    }
}
