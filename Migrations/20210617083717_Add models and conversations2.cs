using Microsoft.EntityFrameworkCore.Migrations;

namespace FalcoBackEnd.Migrations
{
    public partial class Addmodelsandconversations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationConverastion_id",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationConverastion_id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationConverastion_id",
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
                principalColumn: "Converastion_id",
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
                name: "ConversationConverastion_id",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationConverastion_id",
                table: "Messages",
                column: "ConversationConverastion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationConverastion_id",
                table: "Messages",
                column: "ConversationConverastion_id",
                principalTable: "Conversations",
                principalColumn: "Converastion_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
