﻿// <auto-generated />
using System;
using MachinePortal.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MachinePortal.Migrations
{
    [DbContext(typeof(IdentityContext))]
    partial class IdentityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.DefaultPermission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("defaultPermissionID");

                    b.HasKey("ID");

                    b.HasIndex("defaultPermissionID");

                    b.ToTable("DefaultPermission");
                });

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.MachinePortalUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("DepartmentID");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("JobRole");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Mobile");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.Permission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PermissionName");

                    b.HasKey("ID");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.UserPermission", b =>
                {
                    b.Property<string>("UserID");

                    b.Property<int>("PermissionID");

                    b.Property<string>("MachinePortalUserId");

                    b.HasKey("UserID", "PermissionID");

                    b.HasIndex("MachinePortalUserId");

                    b.HasIndex("PermissionID");

                    b.ToTable("UserPermission");
                });

            modelBuilder.Entity("MachinePortal.Models.Area", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("MachinePortal.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MachinePortal.Models.Device", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("ImagePath");

                    b.Property<string>("Model")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("PartNumber")
                        .HasMaxLength(100);

                    b.Property<double>("Price");

                    b.Property<string>("StockLocation")
                        .HasMaxLength(100);

                    b.Property<string>("Supplier")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("MachinePortal.Models.DeviceDocument", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeviceID");

                    b.Property<string>("Extension");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Path");

                    b.HasKey("ID");

                    b.HasIndex("DeviceID");

                    b.ToTable("DeviceDocument");
                });

            modelBuilder.Entity("MachinePortal.Models.Line", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SectorID");

                    b.HasKey("ID");

                    b.HasIndex("SectorID");

                    b.ToTable("Line");
                });

            modelBuilder.Entity("MachinePortal.Models.Machine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaID");

                    b.Property<int>("AssetNumber");

                    b.Property<int>("CostCenter");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("ImagePath");

                    b.Property<DateTime>("LastPreventiveMaintenance");

                    b.Property<int>("LineID");

                    b.Property<string>("MES_Name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SAP_Name");

                    b.Property<int>("SectorID");

                    b.Property<string>("ServerPath");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("WorkCenter");

                    b.HasKey("ID");

                    b.HasIndex("AreaID");

                    b.HasIndex("LineID");

                    b.HasIndex("SectorID");

                    b.ToTable("Machine");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineComment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<int>("MachineID");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.HasIndex("UserID");

                    b.ToTable("MachineComment");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineDevice", b =>
                {
                    b.Property<int>("MachineID");

                    b.Property<int>("DeviceID");

                    b.HasKey("MachineID", "DeviceID");

                    b.HasIndex("DeviceID");

                    b.ToTable("MachineDevice");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineDocument", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Extension");

                    b.Property<string>("FileName");

                    b.Property<string>("Location");

                    b.Property<int>("MachineID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Path");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("MachineID");

                    b.ToTable("MachineDocument");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineImage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Extension");

                    b.Property<string>("FileName");

                    b.Property<string>("Location");

                    b.Property<int>("MachineID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Path");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("MachineID");

                    b.ToTable("MachineImage");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineResponsible", b =>
                {
                    b.Property<int>("MachineID");

                    b.Property<int>("ResponsibleID");

                    b.HasKey("MachineID", "ResponsibleID");

                    b.HasIndex("ResponsibleID");

                    b.ToTable("MachineResponsible");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineVideo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Extension");

                    b.Property<string>("FileName");

                    b.Property<string>("Location");

                    b.Property<int>("MachineID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Path");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("MachineID");

                    b.ToTable("MachineVideo");
                });

            modelBuilder.Entity("MachinePortal.Models.Password", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartmentID");

                    b.Property<string>("DepartmentName");

                    b.Property<string>("EquipmentDescription");

                    b.Property<string>("EquipmentName");

                    b.Property<string>("Level");

                    b.Property<int>("MachineID");

                    b.Property<string>("MachineName");

                    b.Property<string>("Pass");

                    b.Property<string>("User");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.ToTable("Password");
                });

            modelBuilder.Entity("MachinePortal.Models.Responsible", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartmentID");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("FullName");

                    b.Property<string>("JobRole");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Mobile");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PhotoPath");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Responsible");
                });

            modelBuilder.Entity("MachinePortal.Models.Sector", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaID");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("AreaID");

                    b.ToTable("Sector");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.DefaultPermission", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.Permission", "defaultPermission")
                        .WithMany()
                        .HasForeignKey("defaultPermissionID");
                });

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.MachinePortalUser", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID");
                });

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.UserPermission", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.MachinePortalUser", "MachinePortalUser")
                        .WithMany("UserPermissions")
                        .HasForeignKey("MachinePortalUserId");

                    b.HasOne("MachinePortal.Areas.Identity.Data.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.DeviceDocument", b =>
                {
                    b.HasOne("MachinePortal.Models.Device", "Device")
                        .WithMany("Documents")
                        .HasForeignKey("DeviceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.Line", b =>
                {
                    b.HasOne("MachinePortal.Models.Sector", "Sector")
                        .WithMany("Lines")
                        .HasForeignKey("SectorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.Machine", b =>
                {
                    b.HasOne("MachinePortal.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Models.Line", "Line")
                        .WithMany()
                        .HasForeignKey("LineID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Models.Sector", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.MachineComment", b =>
                {
                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineComments")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Areas.Identity.Data.MachinePortalUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineDevice", b =>
                {
                    b.HasOne("MachinePortal.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineDevices")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.MachineDocument", b =>
                {
                    b.HasOne("MachinePortal.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineDocuments")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.MachineImage", b =>
                {
                    b.HasOne("MachinePortal.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineImages")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.MachineResponsible", b =>
                {
                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineResponsibles")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Models.Responsible", "Responsible")
                        .WithMany()
                        .HasForeignKey("ResponsibleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.MachineVideo", b =>
                {
                    b.HasOne("MachinePortal.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineVideos")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.Password", b =>
                {
                    b.HasOne("MachinePortal.Models.Machine")
                        .WithMany("Passwords")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.Responsible", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.Sector", b =>
                {
                    b.HasOne("MachinePortal.Models.Area", "Area")
                        .WithMany("Sectors")
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.MachinePortalUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.MachinePortalUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MachinePortal.Areas.Identity.Data.MachinePortalUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.MachinePortalUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
