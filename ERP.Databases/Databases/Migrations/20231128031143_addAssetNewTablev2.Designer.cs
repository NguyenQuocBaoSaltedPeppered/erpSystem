﻿// <auto-generated />
using System;
using ERP.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ERP.Databases.Databases.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231128031143_addAssetNewTablev2")]
    partial class addAssetNewTablev2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "ltree");
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "unaccent");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ERP.Databases.Schemas.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AssetTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("AssetUnitId")
                        .HasColumnType("integer");

                    b.Property<int?>("BranchId")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ConditionApplyGuarantee")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateBuy")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("text");

                    b.Property<decimal?>("DepreciatedMoney")
                        .HasColumnType("numeric");

                    b.Property<double>("DepreciatedMoneyByMonth")
                        .HasColumnType("double precision");

                    b.Property<decimal?>("DepreciatedMoneyRemain")
                        .HasColumnType("numeric");

                    b.Property<double?>("DepreciatedMonth")
                        .HasColumnType("double precision");

                    b.Property<double?>("DepreciatedRate")
                        .HasColumnType("double precision");

                    b.Property<double?>("DepreciatedValue")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("DepreciationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FinancialCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("GuaranteeTime")
                        .HasColumnType("integer");

                    b.Property<int?>("ManufactureYear")
                        .HasColumnType("integer");

                    b.Property<string>("ManufacturerCode")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<decimal?>("OriginalPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("PurchasePrice")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityAllocated")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityBroken")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityCancel")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityGuarantee")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityLost")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityRemain")
                        .HasColumnType("integer");

                    b.Property<string>("Serial")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("TransferCount")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Vendor")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssetTypeId");

                    b.HasIndex("AssetUnitId");

                    b.ToTable("SYSAST");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.AssetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SYSASTTP");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.AssetUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SYSASTU");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SYSBR");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Department", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("BranchId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("SYSDPM");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Position", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("SYSPOS");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BranchId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DelFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PositionId")
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("PositionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Asset", b =>
                {
                    b.HasOne("ERP.Databases.Schemas.AssetType", "AssetType")
                        .WithMany("Assets")
                        .HasForeignKey("AssetTypeId");

                    b.HasOne("ERP.Databases.Schemas.AssetUnit", "AssetUnit")
                        .WithMany("Assets")
                        .HasForeignKey("AssetUnitId");

                    b.Navigation("AssetType");

                    b.Navigation("AssetUnit");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Department", b =>
                {
                    b.HasOne("ERP.Databases.Schemas.Branch", "Branch")
                        .WithMany("Departments")
                        .HasForeignKey("BranchId");

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Position", b =>
                {
                    b.HasOne("ERP.Databases.Schemas.Department", "Department")
                        .WithMany("Positions")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.User", b =>
                {
                    b.HasOne("ERP.Databases.Schemas.Branch", "Branch")
                        .WithMany("Users")
                        .HasForeignKey("BranchId");

                    b.HasOne("ERP.Databases.Schemas.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("ERP.Databases.Schemas.Employee", "Employee")
                        .WithOne("User")
                        .HasForeignKey("ERP.Databases.Schemas.User", "EmployeeId");

                    b.HasOne("ERP.Databases.Schemas.Position", "Position")
                        .WithMany("Users")
                        .HasForeignKey("PositionId");

                    b.Navigation("Branch");

                    b.Navigation("Department");

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.AssetType", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.AssetUnit", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Branch", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Department", b =>
                {
                    b.Navigation("Positions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Employee", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("ERP.Databases.Schemas.Position", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}