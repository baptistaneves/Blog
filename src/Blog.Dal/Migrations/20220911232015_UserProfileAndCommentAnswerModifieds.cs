using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Dal.Migrations
{
    public partial class UserProfileAndCommentAnswerModifieds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentResponses_PostComments_PostCommentId",
                table: "CommentResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentResponses_UsersProfiles_UserProfileId",
                table: "CommentResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentResponses",
                table: "CommentResponses");

            migrationBuilder.RenameTable(
                name: "CommentResponses",
                newName: "CommentAnswers");

            migrationBuilder.RenameColumn(
                name: "BasicInfo_Phone",
                table: "UsersProfiles",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "BasicInfo_LastName",
                table: "UsersProfiles",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "BasicInfo_FirstName",
                table: "UsersProfiles",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "BasicInfo_EmailAddress",
                table: "UsersProfiles",
                newName: "EmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_CommentResponses_UserProfileId",
                table: "CommentAnswers",
                newName: "IX_CommentAnswers_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentResponses_PostCommentId",
                table: "CommentAnswers",
                newName: "IX_CommentAnswers_PostCommentId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "UsersProfiles",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UsersProfiles",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UsersProfiles",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "UsersProfiles",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentAnswers",
                table: "CommentAnswers",
                column: "CommentAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAnswers_PostComments_PostCommentId",
                table: "CommentAnswers",
                column: "PostCommentId",
                principalTable: "PostComments",
                principalColumn: "PostCommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAnswers_UsersProfiles_UserProfileId",
                table: "CommentAnswers",
                column: "UserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentAnswers_PostComments_PostCommentId",
                table: "CommentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentAnswers_UsersProfiles_UserProfileId",
                table: "CommentAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentAnswers",
                table: "CommentAnswers");

            migrationBuilder.RenameTable(
                name: "CommentAnswers",
                newName: "CommentResponses");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "UsersProfiles",
                newName: "BasicInfo_Phone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "UsersProfiles",
                newName: "BasicInfo_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "UsersProfiles",
                newName: "BasicInfo_FirstName");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "UsersProfiles",
                newName: "BasicInfo_EmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswers_UserProfileId",
                table: "CommentResponses",
                newName: "IX_CommentResponses_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswers_PostCommentId",
                table: "CommentResponses",
                newName: "IX_CommentResponses_PostCommentId");

            migrationBuilder.AlterColumn<string>(
                name: "BasicInfo_Phone",
                table: "UsersProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "BasicInfo_LastName",
                table: "UsersProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "BasicInfo_FirstName",
                table: "UsersProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "BasicInfo_EmailAddress",
                table: "UsersProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentResponses",
                table: "CommentResponses",
                column: "CommentAnswerId");

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
    }
}
