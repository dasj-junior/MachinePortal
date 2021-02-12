using Microsoft.EntityFrameworkCore.Migrations;

namespace MachinePortal.Migrations
{
    public partial class Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineComment_Machine_MachineID",
                table: "MachineComment");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineDocument_Machine_MachineID",
                table: "MachineDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineImage_Machine_MachineID",
                table: "MachineImage");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineVideo_Machine_MachineID",
                table: "MachineVideo");

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineVideo",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineImage",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineDocument",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineComment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineComment_Machine_MachineID",
                table: "MachineComment",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineDocument_Machine_MachineID",
                table: "MachineDocument",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineImage_Machine_MachineID",
                table: "MachineImage",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineVideo_Machine_MachineID",
                table: "MachineVideo",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineComment_Machine_MachineID",
                table: "MachineComment");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineDocument_Machine_MachineID",
                table: "MachineDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineImage_Machine_MachineID",
                table: "MachineImage");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineVideo_Machine_MachineID",
                table: "MachineVideo");

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineVideo",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineImage",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineDocument",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MachineID",
                table: "MachineComment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MachineComment_Machine_MachineID",
                table: "MachineComment",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineDocument_Machine_MachineID",
                table: "MachineDocument",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineImage_Machine_MachineID",
                table: "MachineImage",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineVideo_Machine_MachineID",
                table: "MachineVideo",
                column: "MachineID",
                principalTable: "Machine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
