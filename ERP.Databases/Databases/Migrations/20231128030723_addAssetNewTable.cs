using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ERP.Databases.Databases.Migrations
{
    /// <inheritdoc />
    public partial class addAssetNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedIp = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedIp = table.Column<string>(type: "text", nullable: false),
                    DelFlag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYSASTTP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedIp = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedIp = table.Column<string>(type: "text", nullable: false),
                    DelFlag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSASTTP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYSAST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FinancialCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateBuy = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GuaranteeTime = table.Column<int>(type: "integer", nullable: true),
                    Vendor = table.Column<string>(type: "text", nullable: true),
                    ManufacturerCode = table.Column<string>(type: "text", nullable: true),
                    ManufactureYear = table.Column<int>(type: "integer", nullable: true),
                    Serial = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ConditionApplyGuarantee = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: true),
                    DepreciationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DepreciatedValue = table.Column<double>(type: "double precision", nullable: true),
                    DepreciatedMoneyByMonth = table.Column<double>(type: "double precision", nullable: false),
                    DepreciatedMonth = table.Column<double>(type: "double precision", nullable: true),
                    DepreciatedMoney = table.Column<decimal>(type: "numeric", nullable: true),
                    DepreciatedMoneyRemain = table.Column<decimal>(type: "numeric", nullable: true),
                    DepreciatedRate = table.Column<double>(type: "double precision", nullable: true),
                    TransferCount = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    QuantityAllocated = table.Column<int>(type: "integer", nullable: false),
                    QuantityRemain = table.Column<int>(type: "integer", nullable: false),
                    QuantityBroken = table.Column<int>(type: "integer", nullable: false),
                    QuantityCancel = table.Column<int>(type: "integer", nullable: false),
                    QuantityGuarantee = table.Column<int>(type: "integer", nullable: false),
                    QuantityLost = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedIp = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedIp = table.Column<string>(type: "text", nullable: false),
                    DelFlag = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    AssetTypeId = table.Column<int>(type: "integer", nullable: true),
                    AssetUnitId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSAST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSAST_AssetUnits_AssetUnitId",
                        column: x => x.AssetUnitId,
                        principalTable: "AssetUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SYSAST_SYSASTTP_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "SYSASTTP",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SYSAST_AssetTypeId",
                table: "SYSAST",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSAST_AssetUnitId",
                table: "SYSAST",
                column: "AssetUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYSAST");

            migrationBuilder.DropTable(
                name: "AssetUnits");

            migrationBuilder.DropTable(
                name: "SYSASTTP");
        }
    }
}
