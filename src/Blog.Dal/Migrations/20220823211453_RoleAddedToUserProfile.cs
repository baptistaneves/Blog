using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Dal.Migrations
{
    public partial class RoleAddedToUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UsersProfiles",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UsersProfiles");
        }
    }
}
