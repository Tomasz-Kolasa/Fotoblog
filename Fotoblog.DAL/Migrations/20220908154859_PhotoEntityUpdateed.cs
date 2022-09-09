using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fotoblog.DAL.Migrations
{
    public partial class PhotoEntityUpdateed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "PhotoEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "PhotoEntities");
        }
    }
}
