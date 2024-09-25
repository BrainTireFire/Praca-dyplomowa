using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AAAAAAAAAAAAAAAA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropTable(
                name: "CharacterEffectInstance");

            migrationBuilder.DropTable(
                name: "ChoiceGroupUsageEffectInstance");

            migrationBuilder.DropColumn(
                name: "SourceName",
                table: "EffectInstances");

            migrationBuilder.RenameColumn(
                name: "EffectType",
                table: "EffectInstances",
                newName: "HasNoEffectInCombat");

            migrationBuilder.RenameColumn(
                name: "IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "InitiativeEffectBlueprint_DiceSetId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectBlueprints_IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "IX_EffectBlueprints_InitiativeEffectBlueprint_DiceSetId");

            migrationBuilder.AddColumn<int>(
                name: "R_GrantedThroughId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_TargetedCharacterId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RollMoment",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_GrantedThroughId",
                table: "EffectInstances",
                column: "R_GrantedThroughId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_TargetedCharacterId",
                table: "EffectInstances",
                column: "R_TargetedCharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "InitiativeEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Characters_R_TargetedCharacterId",
                table: "EffectInstances",
                column: "R_TargetedCharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances",
                column: "R_GrantedThroughId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_DiceSet_InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Characters_R_TargetedCharacterId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_R_GrantedThroughId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_R_TargetedCharacterId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "R_GrantedThroughId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "R_TargetedCharacterId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "RollMoment",
                table: "EffectBlueprints");

            migrationBuilder.RenameColumn(
                name: "HasNoEffectInCombat",
                table: "EffectInstances",
                newName: "EffectType");

            migrationBuilder.RenameColumn(
                name: "InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "IniativeEffectBlueprint_DiceSetId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectBlueprints_InitiativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                newName: "IX_EffectBlueprints_IniativeEffectBlueprint_DiceSetId");

            migrationBuilder.AddColumn<string>(
                name: "SourceName",
                table: "EffectInstances",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CharacterEffectInstance",
                columns: table => new
                {
                    R_AffectedById = table.Column<int>(type: "INTEGER", nullable: false),
                    R_TargetedCharactersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEffectInstance", x => new { x.R_AffectedById, x.R_TargetedCharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterEffectInstance_Characters_R_TargetedCharactersId",
                        column: x => x.R_TargetedCharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEffectInstance_EffectInstances_R_AffectedById",
                        column: x => x.R_AffectedById,
                        principalTable: "EffectInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsageEffectInstance",
                columns: table => new
                {
                    R_EffectsGrantedId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_GrantedThroughId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsageEffectInstance", x => new { x.R_EffectsGrantedId, x.R_GrantedThroughId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsageEffectInstance_ChoiceGroupUsages_R_GrantedThroughId",
                        column: x => x.R_GrantedThroughId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsageEffectInstance_EffectInstances_R_EffectsGrantedId",
                        column: x => x.R_EffectsGrantedId,
                        principalTable: "EffectInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEffectInstance_R_TargetedCharactersId",
                table: "CharacterEffectInstance",
                column: "R_TargetedCharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsageEffectInstance_R_GrantedThroughId",
                table: "ChoiceGroupUsageEffectInstance",
                column: "R_GrantedThroughId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_DiceSet_IniativeEffectBlueprint_DiceSetId",
                table: "EffectBlueprints",
                column: "IniativeEffectBlueprint_DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
