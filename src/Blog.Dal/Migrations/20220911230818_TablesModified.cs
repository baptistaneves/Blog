using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Dal.Migrations
{
    public partial class TablesModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostCommentResponseId",
                table: "PostCommentResponses",
                newName: "CommentAnswerId");

            migrationBuilder.RenameColumn(
                name: "PostCommentReactionId",
                table: "PostCommentReactions",
                newName: "CommentReactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentAnswerId",
                table: "PostCommentResponses",
                newName: "PostCommentResponseId");

            migrationBuilder.RenameColumn(
                name: "CommentReactionId",
                table: "PostCommentReactions",
                newName: "PostCommentReactionId");
        }
    }
}
