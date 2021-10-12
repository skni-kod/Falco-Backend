using Microsoft.EntityFrameworkCore.Migrations;

namespace FalcoBackEnd.Migrations
{
    public partial class updateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_Conversation_id",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Conversation_id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Owners",
                table: "Conversations");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ConversationConverastionId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConversationUser",
                columns: table => new
                {
                    ConversationsConverastionId = table.Column<int>(type: "int", nullable: false),
                    OwnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationUser", x => new { x.ConversationsConverastionId, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_ConversationUser_Conversations_ConversationsConverastionId",
                        column: x => x.ConversationsConverastionId,
                        principalTable: "Conversations",
                        principalColumn: "ConverastionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationUser_Users_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationConverastionId",
                table: "Messages",
                column: "ConversationConverastionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationUser_OwnersId",
                table: "ConversationUser",
                column: "OwnersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationConverastionId",
                table: "Messages",
                column: "ConversationConverastionId",
                principalTable: "Conversations",
                principalColumn: "ConverastionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationConverastionId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "ConversationUser");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationConverastionId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationConverastionId",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owners",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Conversation_id",
                table: "Messages",
                column: "Conversation_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_Conversation_id",
                table: "Messages",
                column: "Conversation_id",
                principalTable: "Conversations",
                principalColumn: "ConverastionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
