using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class VariousFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_DiceSet_UsedHitDiceId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassLevels_DiceSet_HitDieId",
                table: "ClassLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_DiceSet_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons");

            migrationBuilder.DropForeignKey(
                name: "FK_Powers_ImmaterialResourceBlueprints_R_UsesImmaterialResourc~",
                table: "Powers");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_DiceSet_DamageValueId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_DamageValueId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_MeleeWeapons_VersatileDamageValueId",
                table: "MeleeWeapons");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_DiceSetId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_R_CreatedByEquippingId",
                table: "EffectBlueprints");

            migrationBuilder.DropIndex(
                name: "IX_ClassLevels_HitDieId",
                table: "ClassLevels");

            migrationBuilder.DropIndex(
                name: "IX_Classes_MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Characters_UsedHitDiceId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "DamageValueId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "VersatileDamageValueId",
                table: "MeleeWeapons");

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

            migrationBuilder.DropColumn(
                name: "HitDieId",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "UsedHitDiceId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "R_Character_UsedHitDiceId",
                table: "DiceSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_ClassLevel_HitDiceId",
                table: "DiceSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_Class_SpellFormulaId",
                table: "DiceSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_MeleeWeapon_VersatileDamageId",
                table: "DiceSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_ValueEffectInstanceId",
                table: "DiceSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_Weapon_DamageId",
                table: "DiceSet",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiceSetId",
                table: "AdditionalValue",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_Character_UsedHitDiceId",
                table: "DiceSet",
                column: "R_Character_UsedHitDiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_Class_SpellFormulaId",
                table: "DiceSet",
                column: "R_Class_SpellFormulaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_ClassLevel_HitDiceId",
                table: "DiceSet",
                column: "R_ClassLevel_HitDiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_MeleeWeapon_VersatileDamageId",
                table: "DiceSet",
                column: "R_MeleeWeapon_VersatileDamageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_ValueEffectInstanceId",
                table: "DiceSet",
                column: "R_ValueEffectInstanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiceSet_R_Weapon_DamageId",
                table: "DiceSet",
                column: "R_Weapon_DamageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_Characters_R_Character_UsedHitDiceId",
                table: "DiceSet",
                column: "R_Character_UsedHitDiceId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_ClassLevels_R_ClassLevel_HitDiceId",
                table: "DiceSet",
                column: "R_ClassLevel_HitDiceId",
                principalTable: "ClassLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_Classes_R_Class_SpellFormulaId",
                table: "DiceSet",
                column: "R_Class_SpellFormulaId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_EffectInstances_R_ValueEffectInstanceId",
                table: "DiceSet",
                column: "R_ValueEffectInstanceId",
                principalTable: "EffectInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_MeleeWeapons_R_MeleeWeapon_VersatileDamageId",
                table: "DiceSet",
                column: "R_MeleeWeapon_VersatileDamageId",
                principalTable: "MeleeWeapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiceSet_Weapons_R_Weapon_DamageId",
                table: "DiceSet",
                column: "R_Weapon_DamageId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances",
                column: "R_GrantedThroughId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Powers_ImmaterialResourceBlueprints_R_UsesImmaterialResourc~",
                table: "Powers",
                column: "R_UsesImmaterialResourceId",
                principalTable: "ImmaterialResourceBlueprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue");

            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_Characters_R_Character_UsedHitDiceId",
                table: "DiceSet");

            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_ClassLevels_R_ClassLevel_HitDiceId",
                table: "DiceSet");

            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_Classes_R_Class_SpellFormulaId",
                table: "DiceSet");

            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_EffectInstances_R_ValueEffectInstanceId",
                table: "DiceSet");

            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_MeleeWeapons_R_MeleeWeapon_VersatileDamageId",
                table: "DiceSet");

            migrationBuilder.DropForeignKey(
                name: "FK_DiceSet_Weapons_R_Weapon_DamageId",
                table: "DiceSet");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ChoiceGroupUsages_R_GrantedThroughId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_Powers_ImmaterialResourceBlueprints_R_UsesImmaterialResourc~",
                table: "Powers");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_Character_UsedHitDiceId",
                table: "DiceSet");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_Class_SpellFormulaId",
                table: "DiceSet");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_ClassLevel_HitDiceId",
                table: "DiceSet");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_MeleeWeapon_VersatileDamageId",
                table: "DiceSet");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_ValueEffectInstanceId",
                table: "DiceSet");

            migrationBuilder.DropIndex(
                name: "IX_DiceSet_R_Weapon_DamageId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_Character_UsedHitDiceId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_ClassLevel_HitDiceId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_Class_SpellFormulaId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_MeleeWeapon_VersatileDamageId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_ValueEffectInstanceId",
                table: "DiceSet");

            migrationBuilder.DropColumn(
                name: "R_Weapon_DamageId",
                table: "DiceSet");

            migrationBuilder.AddColumn<int>(
                name: "DamageValueId",
                table: "Weapons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersatileDamageValueId",
                table: "MeleeWeapons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "HitDieId",
                table: "ClassLevels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumPreparedSpellsFormulaId",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedHitDiceId",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DiceSetId",
                table: "AdditionalValue",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_DamageValueId",
                table: "Weapons",
                column: "DamageValueId");

            migrationBuilder.CreateIndex(
                name: "IX_MeleeWeapons_VersatileDamageValueId",
                table: "MeleeWeapons",
                column: "VersatileDamageValueId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_DiceSetId",
                table: "EffectInstances",
                column: "DiceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_R_CreatedByEquippingId",
                table: "EffectBlueprints",
                column: "R_CreatedByEquippingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevels_HitDieId",
                table: "ClassLevels",
                column: "HitDieId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UsedHitDiceId",
                table: "Characters",
                column: "UsedHitDiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalValue_DiceSet_DiceSetId",
                table: "AdditionalValue",
                column: "DiceSetId",
                principalTable: "DiceSet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_DiceSet_UsedHitDiceId",
                table: "Characters",
                column: "UsedHitDiceId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassLevels_DiceSet_HitDieId",
                table: "ClassLevels",
                column: "HitDieId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_MeleeWeapons_DiceSet_VersatileDamageValueId",
                table: "MeleeWeapons",
                column: "VersatileDamageValueId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Powers_ImmaterialResourceBlueprints_R_UsesImmaterialResourc~",
                table: "Powers",
                column: "R_UsesImmaterialResourceId",
                principalTable: "ImmaterialResourceBlueprints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_DiceSet_DamageValueId",
                table: "Weapons",
                column: "DamageValueId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
