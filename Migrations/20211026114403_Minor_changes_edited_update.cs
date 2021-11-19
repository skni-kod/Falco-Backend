using Microsoft.EntityFrameworkCore.Migrations;

namespace FalcoBackEnd.Migrations
{
    public partial class Minor_changes_edited_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Edited",
                table: "Messages",
                newName: "IsEdited");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Messages",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEdited",
                table: "Messages",
                newName: "Edited");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Messages",
                newName: "Deleted");
        }
    }
}
