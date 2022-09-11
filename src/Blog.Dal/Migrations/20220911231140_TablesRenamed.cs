using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Dal.Migrations
{
    public partial class TablesRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReactions_PostComments_PostCommentId",
                table: "PostCommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentReactions_UsersProfiles_UserProfileId",
                table: "PostCommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentResponses_PostComments_PostCommentId",
                table: "PostCommentResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCommentResponses_UsersProfiles_UserProfileId",
                table: "PostCommentResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCommentResponses",
                table: "PostCommentResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCommentReactions",
                table: "PostCommentReactions");

            migrationBuilder.RenameTable(
                name: "PostCommentResponses",
                newName: "CommentResponses");

            migrationBuilder.RenameTable(
                name: "PostCommentReactions",
                newName: "CommentReactions");

            migrationBuilder.RenameIndex(
                name: "IX_PostCommentResponses_UserProfileId",
                table: "CommentResponses",
                newName: "IX_CommentResponses_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCommentResponses_PostCommentId",
                table: "CommentResponses",
                newName: "IX_CommentResponses_PostCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCommentReactions_UserProfileId",
                table: "CommentReactions",
                newName: "IX_CommentReactions_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PostCommentReactions_PostCommentId",
                table: "CommentReactions",
                newName: "IX_CommentReactions_PostCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentResponses",
                table: "CommentResponses",
                column: "CommentAnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions",
                column: "CommentReactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactions_PostComments_PostCommentId",
                table: "CommentReactions",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "PostCommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReactions_UsersProfiles_UserProfileId",
                table: "CommentReactions",
                column: "UserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentResponses_PostComments_PostCommentId",
                table: "CommentResponses",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "PostCommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentResponses_UsersProfiles_UserProfileId",
                table: "CommentResponses",
                column: "UserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_PostComments_PostCommentId",
                table: "CommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentReactions_UsersProfiles_UserProfileId",
                table: "CommentReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentResponses_PostComments_PostCommentId",
                table: "CommentResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentResponses_UsersProfiles_UserProfileId",
                table: "CommentResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentResponses",
                table: "CommentResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentReactions",
                table: "CommentReactions");

            migrationBuilder.RenameTable(
                name: "CommentResponses",
                newName: "PostCommentResponses");

            migrationBuilder.RenameTable(
                name: "CommentReactions",
                newName: "PostCommentReactions");

            migrationBuilder.RenameIndex(
                name: "IX_CommentResponses_UserProfileId",
                table: "PostCommentResponses",
                newName: "IX_PostCommentResponses_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentResponses_PostCommentId",
                table: "PostCommentResponses",
                newName: "IX_PostCommentResponses_PostCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReactions_UserProfileId",
                table: "PostCommentReactions",
                newName: "IX_PostCommentReactions_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReactions_PostCommentId",
                table: "PostCommentReactions",
                newName: "IX_PostCommentReactions_PostCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCommentResponses",
                table: "PostCommentResponses",
                column: "CommentAnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCommentReactions",
                table: "PostCommentReactions",
                column: "CommentReactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReactions_PostComments_PostCommentId",
                table: "PostCommentReactions",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "PostCommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentReactions_UsersProfiles_UserProfileId",
                table: "PostCommentReactions",
                column: "UserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentResponses_PostComments_PostCommentId",
                table: "PostCommentResponses",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "PostCommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCommentResponses_UsersProfiles_UserProfileId",
                table: "PostCommentResponses",
                column: "UserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
