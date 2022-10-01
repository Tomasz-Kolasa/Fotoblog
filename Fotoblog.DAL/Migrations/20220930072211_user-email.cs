using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fotoblog.DAL.Migrations
{
    public partial class useremail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailVerificationCode",
                table: "UserEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                table: "UserEntities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "EmailVerificationCode",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                table: "UserEntities");
        }
    }
}
