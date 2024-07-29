using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class relationshipNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlueprintId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Races",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FailedDeathSavingThrows",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SucceededDeathSavingThrows",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "FailedDeathSavingThrows",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "SucceededDeathSavingThrows",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "BlueprintId",
                table: "ImmaterialResourceInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
