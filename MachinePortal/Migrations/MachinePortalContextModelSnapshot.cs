﻿// <auto-generated />
using System;
using MachinePortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MachinePortal.Migrations
{
    [DbContext(typeof(MachinePortalContext))]
    partial class MachinePortalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MachinePortal.Areas.Identity.Data.MachinePortalUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Department");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("JobRole");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Mobile");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("MachinePortalUser");
                });

            modelBuilder.Entity("MachinePortal.Models.Area", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("MachinePortal.Models.Device", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Model");

                    b.Property<string>("Name");

                    b.Property<string>("PartNumber");

                    b.Property<double>("Price");

                    b.Property<string>("Supplier");

                    b.HasKey("ID");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("MachinePortal.Models.DeviceDocument", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeviceID");

                    b.Property<string>("Extension");

                    b.Property<string>("Name");

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

                    b.Property<string>("Name");

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

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<int>("LineID");

                    b.Property<string>("MES_Name");

                    b.Property<string>("Name");

                    b.Property<string>("SAP_Name");

                    b.Property<int>("SectorID");

                    b.Property<string>("ServerPath");

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

                    b.Property<string>("Author");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("Date");

                    b.Property<int>("MachineID");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.ToTable("MachineComments");
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

                    b.Property<string>("Category");

                    b.Property<string>("Extension");

                    b.Property<int>("MachineID");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.ToTable("MachineDocument");
                });

            modelBuilder.Entity("MachinePortal.Models.MachineImage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Extension");

                    b.Property<int>("MachineID");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("ID");

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

                    b.Property<string>("Extension");

                    b.Property<int>("MachineID");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.ToTable("MachineVideo");
                });

            modelBuilder.Entity("MachinePortal.Models.Permission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PermissionName");

                    b.HasKey("ID");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("MachinePortal.Models.Responsible", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Department");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("FullName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mobile");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PhotoPath");

                    b.HasKey("ID");

                    b.ToTable("Responsible");
                });

            modelBuilder.Entity("MachinePortal.Models.Sector", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaID");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("AreaID");

                    b.ToTable("Sector");
                });

            modelBuilder.Entity("MachinePortal.Models.UserPermission", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("PermissionID");

                    b.Property<string>("MachinePortalUserId");

                    b.HasKey("UserID", "PermissionID");

                    b.HasIndex("MachinePortalUserId");

                    b.HasIndex("PermissionID");

                    b.ToTable("UserPermission");
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
                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineDocuments")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.MachineImage", b =>
                {
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
                    b.HasOne("MachinePortal.Models.Machine", "Machine")
                        .WithMany("MachineVideos")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.Sector", b =>
                {
                    b.HasOne("MachinePortal.Models.Area", "Area")
                        .WithMany("Sectors")
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MachinePortal.Models.UserPermission", b =>
                {
                    b.HasOne("MachinePortal.Areas.Identity.Data.MachinePortalUser", "MachinePortalUser")
                        .WithMany("UserPermissions")
                        .HasForeignKey("MachinePortalUserId");

                    b.HasOne("MachinePortal.Models.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
