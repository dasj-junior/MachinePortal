using Microsoft.EntityFrameworkCore.Migrations;

namespace MachinePortal.Migrations.Identity
{
    public partial class Permissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MachinePortalUserID",
                table: "UserPermission",
                newName: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserPermission",
                newName: "MachinePortalUserID");
        }
    }
}
