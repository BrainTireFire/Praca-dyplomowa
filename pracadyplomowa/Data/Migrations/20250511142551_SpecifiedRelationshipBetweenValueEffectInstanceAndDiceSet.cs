using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpecifiedRelationshipBetweenValueEffectInstanceAndDiceSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_R_CreatedByEquippingId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "EffectType_SavingThrowEffect_Condition",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "EffectType_SavingThrowEffect_Nature",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSetId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Condition",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "SavingThrowEffectType_SavingThrowEffect_Nature",
                table: "EffectBlueprints");

            migrationBuilder.AddColumn<int>(
                name: "R_ValueEffectInstanceId",
                table: "DiceSet",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_ValueEffectInstanceId",
                table: "DiceSet",
                column: "R_ValueEffectInstanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_EffectInstances_R_ValueEffectInstanceId",
                table: "DiceSet",
                column: "R_ValueEffectInstanceId",
                principalTable: "EffectInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances",
                column: "R_GrantedThroughId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_EffectInstances_R_ValueEffectInstanceId",
                table: "DiceSet");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_ValueEffectInstanceId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_ValueEffectInstanceId",
                table: "DiceSet");

            migrationBuilder.AddColumn<int>(
                name: "DiceSetId",
                table: "EffectInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EffectType_SavingThrowEffect_Condition",
                table: "EffectInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EffectType_SavingThrowEffect_Nature",
                table: "EffectInstances",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiceSetId",
                table: "EffectBlueprints",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Condition",
                table: "EffectBlueprints",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavingThrowEffectType_SavingThrowEffect_Nature",
                table: "EffectBlueprints",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_DiceSetId",
                table: "EffectInstances",
                column: "DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_CreatedByEquippingId",
                table: "EffectBlueprints",
                column: "R_CreatedByEquippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints",
                column: "R_CreatedByEquippingId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances",
                column: "R_GrantedThroughId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_DiceSet_DiceSetId",
                table: "EffectInstances",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
