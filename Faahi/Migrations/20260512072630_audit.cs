using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Faahi.Migrations
{
    /// <inheritdoc />
    public partial class audit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            migrationBuilder.CreateTable(
                name: "im_AuditLogs",
                columns: table => new
                {
                    audit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    action_type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    field_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    old_value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    new_value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    changedby_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    changed_by_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    changed_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    remarks = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_AuditLogs", x => x.audit_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_AuditLogs",
                table: "im_AuditLogs",
                columns: new[] { "audit_id", "business_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_AuditLogs");
        }
    }
}
