using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AAAAAAAAAAAAAAAAA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Languages_LanguageId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Languages_LanguageId",
                table: "EffectInstances");

            migrationBuilder.RenameColumn(
                name: "LanguageId",
                table: "EffectInstances",
                newName: "R_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectInstances_LanguageId",
                table: "EffectInstances",
                newName: "IX_EffectInstances_R_LanguageId");

            migrationBuilder.RenameColumn(
                name: "LanguageId",
                table: "EffectBlueprints",
                newName: "R_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectBlueprints_LanguageId",
                table: "EffectBlueprints",
                newName: "IX_EffectBlueprints_R_LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Languages_R_LanguageId",
                table: "EffectBlueprints",
                column: "R_LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Languages_R_LanguageId",
                table: "EffectInstances",
                column: "R_LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Languages_R_LanguageId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Languages_R_LanguageId",
                table: "EffectInstances");

            migrationBuilder.RenameColumn(
                name: "R_LanguageId",
                table: "EffectInstances",
                newName: "LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectInstances_R_LanguageId",
                table: "EffectInstances",
                newName: "IX_EffectInstances_LanguageId");

            migrationBuilder.RenameColumn(
                name: "R_LanguageId",
                table: "EffectBlueprints",
                newName: "LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectBlueprints_R_LanguageId",
                table: "EffectBlueprints",
                newName: "IX_EffectBlueprints_LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Languages_LanguageId",
                table: "EffectBlueprints",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Languages_LanguageId",
                table: "EffectInstances",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }
    }
}
