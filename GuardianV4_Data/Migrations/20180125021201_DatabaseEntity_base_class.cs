using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GuardianV4_Data.Migrations
{
    public partial class DatabaseEntity_base_class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateAdded",
                table: "Servers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "GuildName",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateAdded",
                table: "Quotes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "GuildName",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Quotes");
        }
    }
}
