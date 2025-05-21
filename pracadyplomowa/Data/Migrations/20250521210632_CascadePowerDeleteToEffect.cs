using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadePowerDeleteToEffect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Powers_R_PowerId",
                table: "EffectBlueprints");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Powers_R_PowerId",
                table: "EffectBlueprints",
                column: "R_PowerId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Powers_R_PowerId",
                table: "EffectBlueprints");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Powers_R_PowerId",
                table: "EffectBlueprints",
                column: "R_PowerId",
                principalTable: "Powers",
                principalColumn: "Id");
        }
    }
}
