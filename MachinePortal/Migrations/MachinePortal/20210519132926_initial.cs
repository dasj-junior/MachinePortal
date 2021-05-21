using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MachinePortal.Migrations.MachinePortal
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Brand = table.Column<string>(maxLength: 50, nullable: true),
                    Model = table.Column<string>(maxLength: 50, nullable: true),
                    PartNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Supplier = table.Column<string>(maxLength: 50, nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Responsible",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsible", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    AreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sector_Area_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Area",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDocument",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    SectorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Line_Sector_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sector",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    AssetNumber = table.Column<int>(nullable: false),
                    MES_Name = table.Column<string>(nullable: true),
                    SAP_Name = table.Column<string>(nullable: true),
                    WorkCenter = table.Column<string>(nullable: true),
                    CostCenter = table.Column<int>(nullable: false),
                    ServerPath = table.Column<string>(nullable: true),
                    AreaID = table.Column<int>(nullable: false),
                    SectorID = table.Column<int>(nullable: false),
                    LineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Machine_Area_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Area",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Machine_Line_LineID",
                        column: x => x.LineID,
                        principalTable: "Line",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Machine_Sector_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sector",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    MachineID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MachineComment_Machine_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineComment_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachineDevice",
                columns: table => new
                {
                    MachineID = table.Column<int>(nullable: false),
                    DeviceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineDevice", x => new { x.MachineID, x.DeviceID });
                    table.ForeignKey(
                        name: "FK_MachineDevice_Device_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineDevice_Machine_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineDocument",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Category = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MachineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineDocument", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MachineDocument_Machine_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineImage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Category = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MachineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MachineImage_Machine_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachinePassword",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipmentName = table.Column<string>(nullable: true),
                    EquipmentDescription = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: true),
                    MachineID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachinePassword", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MachinePassword_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachinePassword_Machine_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachineResponsible",
                columns: table => new
                {
                    MachineID = table.Column<int>(nullable: false),
                    ResponsibleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineResponsible", x => new { x.MachineID, x.ResponsibleID });
                    table.ForeignKey(
                        name: "FK_MachineResponsible_Machine_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineResponsible_Responsible_ResponsibleID",
                        column: x => x.ResponsibleID,
                        principalTable: "Responsible",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineVideo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Category = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MachineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineVideo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MachineVideo_Machine_MachineID",
                        column: x => x.MachineID,
                        principalTable: "Machine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDocument_DeviceID",
                table: "DeviceDocument",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Line_SectorID",
                table: "Line",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_AreaID",
                table: "Machine",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_LineID",
                table: "Machine",
                column: "LineID");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_SectorID",
                table: "Machine",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineComment_MachineID",
                table: "MachineComment",
                column: "MachineID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineComment_UserID",
                table: "MachineComment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineDevice_DeviceID",
                table: "MachineDevice",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineDocument_MachineID",
                table: "MachineDocument",
                column: "MachineID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineImage_MachineID",
                table: "MachineImage",
                column: "MachineID");

            migrationBuilder.CreateIndex(
                name: "IX_MachinePassword_MachineID",
                table: "MachinePassword",
                column: "MachineID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineResponsible_ResponsibleID",
                table: "MachineResponsible",
                column: "ResponsibleID");

            migrationBuilder.CreateIndex(
                name: "IX_MachineVideo_MachineID",
                table: "MachineVideo",
                column: "MachineID");

            migrationBuilder.CreateIndex(
                name: "IX_Sector_AreaID",
                table: "Sector",
                column: "AreaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceDocument");

            migrationBuilder.DropTable(
                name: "MachineComment");

            migrationBuilder.DropTable(
                name: "MachineDevice");

            migrationBuilder.DropTable(
                name: "MachineDocument");

            migrationBuilder.DropTable(
                name: "MachineImage");

            migrationBuilder.DropTable(
                name: "MachinePassword");

            migrationBuilder.DropTable(
                name: "MachineResponsible");

            migrationBuilder.DropTable(
                name: "MachineVideo");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Responsible");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
