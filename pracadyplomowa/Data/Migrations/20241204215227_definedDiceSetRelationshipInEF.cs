using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class definedDiceSetRelationshipInEF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.AddColumn<int>(
                name: "R_ValueEffectBlueprintId",
                table: "DiceSet",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_ValueEffectBlueprintId",
                table: "DiceSet",
                column: "R_ValueEffectBlueprintId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_EffectBlueprints_R_ValueEffectBlueprintId",
                table: "DiceSet",
                column: "R_ValueEffectBlueprintId",
                principalTable: "EffectBlueprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_EffectBlueprints_R_ValueEffectBlueprintId",
                table: "DiceSet");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_ValueEffectBlueprintId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_ValueEffectBlueprintId",
                table: "DiceSet");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_DiceSetId",
                table: "EffectBlueprints",
                column: "DiceSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_DiceSetId",
                table: "EffectBlueprints",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
