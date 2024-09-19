using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Crm
{
    /// <inheritdoc />
    public partial class AuditableEntityChangeV21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "crm",
                table: "crm_customer",
                newName: "ModifiedUserName");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "crm",
                table: "crm_customer",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "crm",
                table: "crm_customer");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "crm",
                table: "crm_customer",
                newName: "CreatedUserStaffName");
        }
    }
}
