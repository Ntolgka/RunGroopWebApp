using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroopWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAndUpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Races",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Races",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Clubs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Clubs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Clubs");
        }
    }
}
