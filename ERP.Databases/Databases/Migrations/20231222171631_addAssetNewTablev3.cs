using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ERP.Databases.Databases.Migrations
{
    /// <inheritdoc />
    public partial class addAssetNewTablev3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DepreciationDate",
                table: "SYSAST",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBuy",
                table: "SYSAST",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SYSASTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ExportDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    TotalMoney = table.Column<double>(type: "double precision", nullable: true),
                    CompensationValue = table.Column<double>(type: "double precision", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
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
                    ResponsibleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSASTE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYSASTI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ImportDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    TotalMoney = table.Column<double>(type: "double precision", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedIp = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedIp = table.Column<string>(type: "text", nullable: false),
                    DelFlag = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSASTI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYSASTST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
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
                    AssetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSASTST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSASTST_SYSAST_AssetId",
                        column: x => x.AssetId,
                        principalTable: "SYSAST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SYSASTIDT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    AssetImportId = table.Column<int>(type: "integer", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedIp = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedIp = table.Column<string>(type: "text", nullable: false),
                    DelFlag = table.Column<bool>(type: "boolean", nullable: false),
                    AssetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSASTIDT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSASTIDT_SYSASTI_AssetImportId",
                        column: x => x.AssetImportId,
                        principalTable: "SYSASTI",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SYSASTIDT_SYSAST_AssetId",
                        column: x => x.AssetId,
                        principalTable: "SYSAST",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SYSASTTF",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetExportId = table.Column<int>(type: "integer", nullable: true),
                    AssetImportId = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    FromBranch = table.Column<int>(type: "integer", nullable: true),
                    ToBranch = table.Column<int>(type: "integer", nullable: true),
                    FromUser = table.Column<int>(type: "integer", nullable: true),
                    ToUser = table.Column<int>(type: "integer", nullable: true),
                    AssetTranferOldId = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    IsAllocationToDepartment = table.Column<bool>(type: "boolean", nullable: true),
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
                    table.PrimaryKey("PK_SYSASTTF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSASTTF_SYSASTE_AssetExportId",
                        column: x => x.AssetExportId,
                        principalTable: "SYSASTE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SYSASTTF_SYSASTI_AssetImportId",
                        column: x => x.AssetImportId,
                        principalTable: "SYSASTI",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SYSASTEDT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetStockId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedIp = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedIp = table.Column<string>(type: "text", nullable: false),
                    DelFlag = table.Column<bool>(type: "boolean", nullable: false),
                    AssetExportId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSASTEDT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSASTEDT_SYSASTE_AssetExportId",
                        column: x => x.AssetExportId,
                        principalTable: "SYSASTE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SYSASTEDT_SYSASTST_AssetStockId",
                        column: x => x.AssetStockId,
                        principalTable: "SYSASTST",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SYSASTHTR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionName = table.Column<string>(type: "text", nullable: true),
                    AcctionCode = table.Column<string>(type: "text", nullable: true),
                    BeginInventory = table.Column<int>(type: "integer", nullable: true),
                    QuantityChange = table.Column<int>(type: "integer", nullable: false),
                    EndQuantity = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ValueInventory = table.Column<double>(type: "double precision", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedIp = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedIp = table.Column<string>(type: "text", nullable: false),
                    DelFlag = table.Column<bool>(type: "boolean", nullable: false),
                    AssetStockId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSASTHTR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYSASTHTR_SYSASTST_AssetStockId",
                        column: x => x.AssetStockId,
                        principalTable: "SYSASTST",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTEDT_AssetExportId",
                table: "SYSASTEDT",
                column: "AssetExportId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTEDT_AssetStockId",
                table: "SYSASTEDT",
                column: "AssetStockId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTHTR_AssetStockId",
                table: "SYSASTHTR",
                column: "AssetStockId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTIDT_AssetId",
                table: "SYSASTIDT",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTIDT_AssetImportId",
                table: "SYSASTIDT",
                column: "AssetImportId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTST_AssetId",
                table: "SYSASTST",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTTF_AssetExportId",
                table: "SYSASTTF",
                column: "AssetExportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYSASTTF_AssetImportId",
                table: "SYSASTTF",
                column: "AssetImportId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYSASTEDT");

            migrationBuilder.DropTable(
                name: "SYSASTHTR");

            migrationBuilder.DropTable(
                name: "SYSASTIDT");

            migrationBuilder.DropTable(
                name: "SYSASTTF");

            migrationBuilder.DropTable(
                name: "SYSASTST");

            migrationBuilder.DropTable(
                name: "SYSASTE");

            migrationBuilder.DropTable(
                name: "SYSASTI");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepreciationDate",
                table: "SYSAST",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBuy",
                table: "SYSAST",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
