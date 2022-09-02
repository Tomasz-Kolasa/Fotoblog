﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fotoblog.DAL.Migrations
{
    public partial class updatemodifiedtimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "PhotoEntities",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TagEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TagEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PhotoEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TagEntities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TagEntities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PhotoEntities");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "PhotoEntities",
                newName: "CreateDate");
        }
    }
}
