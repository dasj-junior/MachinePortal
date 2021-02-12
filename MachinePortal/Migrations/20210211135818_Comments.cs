using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MachinePortal.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineComment_Machine_MachineID",
                table: "MachineComment");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineComment",
                table: "MachineComment");

            migrationBuilder.RenameTable(
                name: "MachineComment",
                newName: "MachineComments");

            migrationBuilder.RenameIndex(
                name: "IX_MachineComment_MachineID",
                table: "MachineComments",
                newName: "IX_MachineComments_MachineID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineComments",
                table: "MachineComments",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "DeviceDocument",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    DeviceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDocument", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeviceDocument_Device_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDocument_DeviceID",
                table: "DeviceDocument",
                column: "DeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineComments_Machine_MachineID",
                table: "MachineComments",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineComments_Machine_MachineID",
                table: "MachineComments");

            migrationBuilder.DropTable(
                name: "DeviceDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineComments",
                table: "MachineComments");

            migrationBuilder.RenameTable(
                name: "MachineComments",
                newName: "MachineComment");

            migrationBuilder.RenameIndex(
                name: "IX_MachineComments_MachineID",
                table: "MachineComment",
                newName: "IX_MachineComment_MachineID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineComment",
                table: "MachineComment",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceID = table.Column<int>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Document_Device_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_DeviceID",
                table: "Document",
                column: "DeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineComment_Machine_MachineID",
                table: "MachineComment",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
