using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class UserStaffRealName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealName",
                schema: "console",
                table: "cs_user_info");

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                schema: "console",
                table: "cs_user_staff_info",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealName",
                schema: "console",
                table: "cs_user_staff_info");

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                schema: "console",
                table: "cs_user_info",
                type: "varchar(16)",
                maxLength: 16,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
