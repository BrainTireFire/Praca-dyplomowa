using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class skopanenazwyrelacji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrantedByClassLevelId",
                table: "ChoiceGroups");

            migrationBuilder.DropColumn(
                name: "GrantedByRaceLevelId",
                table: "ChoiceGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrantedByClassLevelId",
                table: "ChoiceGroups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GrantedByRaceLevelId",
                table: "ChoiceGroups",
                type: "INTEGER",
                nullable: true);
        }
    }
}
