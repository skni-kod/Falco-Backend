using Microsoft.EntityFrameworkCore.Migrations;

namespace FalcoBackEnd.Migrations
{
    public partial class Addmodelsandconversations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationConverastionId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationConverastionId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationConverastionId",
                table: "Messages");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_Conversation_id",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Conversation_id",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "ConversationConverastionId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationConverastionId",
                table: "Messages",
                column: "ConversationConverastionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationConverastionId",
                table: "Messages",
                column: "ConversationConverastionId",
                principalTable: "Conversations",
                principalColumn: "ConverastionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
