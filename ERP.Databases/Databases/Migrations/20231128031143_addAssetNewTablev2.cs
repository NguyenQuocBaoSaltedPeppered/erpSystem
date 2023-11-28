using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Databases.Databases.Migrations
{
    /// <inheritdoc />
    public partial class addAssetNewTablev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SYSAST_AssetUnits_AssetUnitId",
                table: "SYSAST");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetUnits",
                table: "AssetUnits");

            migrationBuilder.RenameTable(
                name: "AssetUnits",
                newName: "SYSASTU");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SYSASTU",
                table: "SYSASTU",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SYSAST_SYSASTU_AssetUnitId",
                table: "SYSAST",
                column: "AssetUnitId",
                principalTable: "SYSASTU",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SYSAST_SYSASTU_AssetUnitId",
                table: "SYSAST");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SYSASTU",
                table: "SYSASTU");

            migrationBuilder.RenameTable(
                name: "SYSASTU",
                newName: "AssetUnits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetUnits",
                table: "AssetUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SYSAST_AssetUnits_AssetUnitId",
                table: "SYSAST",
                column: "AssetUnitId",
                principalTable: "AssetUnits",
                principalColumn: "Id");
        }
    }
}
