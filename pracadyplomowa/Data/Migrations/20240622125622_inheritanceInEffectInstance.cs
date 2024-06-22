using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class inheritanceInEffectInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroupField_EffectGroups_R_EffectOnFieldId",
                table: "EffectGroupField");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroupField_Fields_R_EffectOnFieldId1",
                table: "EffectGroupField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EffectGroupField",
                table: "EffectGroupField");

            migrationBuilder.DropIndex(
                name: "IX_EffectGroupField_R_EffectOnFieldId1",
                table: "EffectGroupField");

            migrationBuilder.RenameColumn(
                name: "R_EffectOnFieldId1",
                table: "EffectGroupField",
                newName: "R_EffectGroupOnFieldId");

            migrationBuilder.RenameColumn(
                name: "diceSet_flat",
                table: "EffectBlueprints",
                newName: "DiceSet_flat");

            migrationBuilder.RenameColumn(
                name: "diceSet_d8",
                table: "EffectBlueprints",
                newName: "DiceSet_d8");

            migrationBuilder.RenameColumn(
                name: "diceSet_d6",
                table: "EffectBlueprints",
                newName: "DiceSet_d6");

            migrationBuilder.RenameColumn(
                name: "diceSet_d4",
                table: "EffectBlueprints",
                newName: "DiceSet_d4");

            migrationBuilder.RenameColumn(
                name: "diceSet_d20",
                table: "EffectBlueprints",
                newName: "DiceSet_d20");

            migrationBuilder.RenameColumn(
                name: "diceSet_d12",
                table: "EffectBlueprints",
                newName: "DiceSet_d12");

            migrationBuilder.RenameColumn(
                name: "diceSet_d100",
                table: "EffectBlueprints",
                newName: "DiceSet_d100");

            migrationBuilder.RenameColumn(
                name: "diceSet_d10",
                table: "EffectBlueprints",
                newName: "DiceSet_d10");

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_d10",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_d100",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_d12",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_d20",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_d4",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_d6",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_d8",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceSet_flat",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "EffectInstances",
                type: "TEXT",
                maxLength: 55,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EffectGroupField",
                table: "EffectGroupField",
                columns: new[] { "R_EffectGroupOnFieldId", "R_EffectOnFieldId" });

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroupField_R_EffectOnFieldId",
                table: "EffectGroupField",
                column: "R_EffectOnFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroupField_EffectGroups_R_EffectGroupOnFieldId",
                table: "EffectGroupField",
                column: "R_EffectGroupOnFieldId",
                principalTable: "EffectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroupField_Fields_R_EffectOnFieldId",
                table: "EffectGroupField",
                column: "R_EffectOnFieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroupField_EffectGroups_R_EffectGroupOnFieldId",
                table: "EffectGroupField");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroupField_Fields_R_EffectOnFieldId",
                table: "EffectGroupField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EffectGroupField",
                table: "EffectGroupField");

            migrationBuilder.DropIndex(
                name: "IX_EffectGroupField_R_EffectOnFieldId",
                table: "EffectGroupField");

            migrationBuilder.DropColumn(
                name: "DiceSet_d10",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSet_d100",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSet_d12",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSet_d20",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSet_d4",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSet_d6",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSet_d8",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "DiceSet_flat",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "EffectInstances");

            migrationBuilder.RenameColumn(
                name: "R_EffectGroupOnFieldId",
                table: "EffectGroupField",
                newName: "R_EffectOnFieldId1");

            migrationBuilder.RenameColumn(
                name: "DiceSet_flat",
                table: "EffectBlueprints",
                newName: "diceSet_flat");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d8",
                table: "EffectBlueprints",
                newName: "diceSet_d8");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d6",
                table: "EffectBlueprints",
                newName: "diceSet_d6");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d4",
                table: "EffectBlueprints",
                newName: "diceSet_d4");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d20",
                table: "EffectBlueprints",
                newName: "diceSet_d20");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d12",
                table: "EffectBlueprints",
                newName: "diceSet_d12");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d100",
                table: "EffectBlueprints",
                newName: "diceSet_d100");

            migrationBuilder.RenameColumn(
                name: "DiceSet_d10",
                table: "EffectBlueprints",
                newName: "diceSet_d10");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EffectGroupField",
                table: "EffectGroupField",
                columns: new[] { "R_EffectOnFieldId", "R_EffectOnFieldId1" });

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroupField_R_EffectOnFieldId1",
                table: "EffectGroupField",
                column: "R_EffectOnFieldId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroupField_EffectGroups_R_EffectOnFieldId",
                table: "EffectGroupField",
                column: "R_EffectOnFieldId",
                principalTable: "EffectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroupField_Fields_R_EffectOnFieldId1",
                table: "EffectGroupField",
                column: "R_EffectOnFieldId1",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
