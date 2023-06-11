using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class SocialLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSocialLogin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSocialLogin",
                table: "Users");
        }
    }
}
