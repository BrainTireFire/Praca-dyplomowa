using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefinedRelationshipBetweenDiceSetAndAdditionalValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue");

            migrationBuilder.AlterColumn<int>(
                name: "DiceSetId",
                table: "AdditionalValue",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue");

            migrationBuilder.AlterColumn<int>(
                name: "DiceSetId",
                table: "AdditionalValue",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id");
        }
    }
}
