using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatingEffectDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "EffectInstances",
                newName: "ProficiencyEffectType_ItemType");

            migrationBuilder.RenameColumn(
                name: "EffectType_SizeBonus",
                table: "EffectInstances",
                newName: "ProficiencyEffectType_ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "EffectType_MovementCost_Multiplier",
                table: "EffectInstances",
                newName: "EffectType_MovementCostEffect");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "EffectBlueprints",
                newName: "ProficiencyEffectType_ItemType");

            migrationBuilder.RenameColumn(
                name: "SizeEffectType_SizeBonus",
                table: "EffectBlueprints",
                newName: "ProficiencyEffectType_ProficiencyEffect");

            migrationBuilder.RenameColumn(
                name: "MovementCostEffectType_MovementCost_Multiplier",
                table: "EffectBlueprints",
                newName: "MovementCostEffectType_MovementCostEffect");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ItemType",
                table: "EffectInstances",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectInstances",
                newName: "EffectType_SizeBonus");

            migrationBuilder.RenameColumn(
                name: "EffectType_MovementCostEffect",
                table: "EffectInstances",
                newName: "EffectType_MovementCost_Multiplier");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ItemType",
                table: "EffectBlueprints",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "ProficiencyEffectType_ProficiencyEffect",
                table: "EffectBlueprints",
                newName: "SizeEffectType_SizeBonus");

            migrationBuilder.RenameColumn(
                name: "MovementCostEffectType_MovementCostEffect",
                table: "EffectBlueprints",
                newName: "MovementCostEffectType_MovementCost_Multiplier");
        }
    }
}
