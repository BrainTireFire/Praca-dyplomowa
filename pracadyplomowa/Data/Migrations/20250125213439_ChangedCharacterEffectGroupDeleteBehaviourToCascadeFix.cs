using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCharacterEffectGroupDeleteBehaviourToCascadeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_R_ConcentratesOnId",
                table: "Characters");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_ConcentratedOnByCharacterId",
                table: "EffectGroups",
                column: "R_ConcentratedOnByCharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroups_Characters_R_ConcentratedOnByCharacterId",
                table: "EffectGroups",
                column: "R_ConcentratedOnByCharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Characters_R_ConcentratedOnByCharacterId",
                table: "EffectGroups");

            migrationBuilder.DropIndex(
                name: "IX_EffectGroups_R_ConcentratedOnByCharacterId",
                table: "EffectGroups");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_ConcentratesOnId",
                table: "Characters",
                column: "R_ConcentratesOnId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                table: "Characters",
                column: "R_ConcentratesOnId",
                principalTable: "EffectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
