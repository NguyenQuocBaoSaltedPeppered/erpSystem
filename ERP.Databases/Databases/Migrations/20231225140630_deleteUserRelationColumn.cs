using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Databases.Databases.Migrations
{
    /// <inheritdoc />
    public partial class deleteUserRelationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionId",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
