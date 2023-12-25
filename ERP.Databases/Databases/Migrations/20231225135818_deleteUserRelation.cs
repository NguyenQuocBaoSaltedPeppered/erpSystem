using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Databases.Databases.Migrations
{
    /// <inheritdoc />
    public partial class deleteUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SYSPOS_PositionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PositionId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PositionId",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionId",
                table: "Employees",
                type: "character varying(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SYSPOS_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "SYSPOS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SYSPOS_PositionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PositionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "PositionId",
                table: "Users",
                type: "character varying(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SYSPOS_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "SYSPOS",
                principalColumn: "Id");
        }
    }
}
