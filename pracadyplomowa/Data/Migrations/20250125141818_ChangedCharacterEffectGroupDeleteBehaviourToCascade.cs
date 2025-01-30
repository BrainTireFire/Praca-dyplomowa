using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCharacterEffectGroupDeleteBehaviourToCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                table: "Characters");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                table: "Characters",
                column: "R_ConcentratesOnId",
                principalTable: "EffectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                table: "Characters");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_EffectGroups_R_ConcentratesOnId",
                table: "Characters",
                column: "R_ConcentratesOnId",
                principalTable: "EffectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
