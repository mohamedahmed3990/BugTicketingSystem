using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTicketingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateattachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Bugs_BugId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Attachments");

            migrationBuilder.AlterColumn<int>(
                name: "BugId",
                table: "Attachments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Bugs_BugId",
                table: "Attachments",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Bugs_BugId",
                table: "Attachments");

            migrationBuilder.AlterColumn<int>(
                name: "BugId",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Bugs_BugId",
                table: "Attachments",
                column: "BugId",
                principalTable: "Bugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
