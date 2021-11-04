using Microsoft.EntityFrameworkCore.Migrations;

namespace FalcoBackEnd.Migrations
{
    public partial class EditDelete_messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Edited",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Messages");
        }
    }
}
