using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAuraOwningEffectGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Auras_R_OriginatesFromAuraId",
                table: "EffectGroups");

            migrationBuilder.DropIndex(
                name: "IX_EffectGroups_R_OriginatesFromAuraId",
                table: "EffectGroups");

            migrationBuilder.DropColumn(
                name: "R_OriginatesFromAuraId",
                table: "EffectGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "R_OriginatesFromAuraId",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_OriginatesFromAuraId",
                table: "EffectGroups",
                column: "R_OriginatesFromAuraId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroups_Auras_R_OriginatesFromAuraId",
                table: "EffectGroups",
                column: "R_OriginatesFromAuraId",
                principalTable: "Auras",
                principalColumn: "Id");
        }
    }
}
