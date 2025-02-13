using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActionLogAddFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ActionLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EncounterId",
                table: "ActionLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "ActionLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "ActionLogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "ActionLogs");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "ActionLogs");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "ActionLogs");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "ActionLogs");
        }
    }
}
