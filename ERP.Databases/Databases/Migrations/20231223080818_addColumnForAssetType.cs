using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Databases.Databases.Migrations
{
    /// <inheritdoc />
    public partial class addColumnForAssetType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "SYSASTTP",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SYSASTTP",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "SYSASTTP",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SYSASTTP",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "SYSASTTP",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "SYSASTTP");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "SYSASTTP");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "SYSASTTP");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SYSASTTP");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "SYSASTTP");
        }
    }
}
