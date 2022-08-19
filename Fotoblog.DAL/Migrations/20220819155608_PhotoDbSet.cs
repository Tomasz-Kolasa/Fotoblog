using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fotoblog.DAL.Migrations
{
    public partial class PhotoDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotosTags_PhotoEntity_PhotosId",
                table: "PhotosTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoEntity",
                table: "PhotoEntity");

            migrationBuilder.RenameTable(
                name: "PhotoEntity",
                newName: "PhotoEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoEntities",
                table: "PhotoEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotosTags_PhotoEntities_PhotosId",
                table: "PhotosTags",
                column: "PhotosId",
                principalTable: "PhotoEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotosTags_PhotoEntities_PhotosId",
                table: "PhotosTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoEntities",
                table: "PhotoEntities");

            migrationBuilder.RenameTable(
                name: "PhotoEntities",
                newName: "PhotoEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoEntity",
                table: "PhotoEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotosTags_PhotoEntity_PhotosId",
                table: "PhotosTags",
                column: "PhotosId",
                principalTable: "PhotoEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
