using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class movedImmaterialResourcesToChoiceGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropTable(
                name: "ClassLevelImmaterialResourceAmount");

            migrationBuilder.DropTable(
                name: "ImmaterialResourceAmountRaceLevel");

            migrationBuilder.RenameColumn(
                name: "R_CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_ImmaterialResourceInstances_R_CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "IX_ImmaterialResourceInstances_CharacterId");

            migrationBuilder.AddColumn<int>(
                name: "R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "R_ChoiceGroupId",
                table: "ImmaterialResourceAmounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceInstances_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                column: "R_ChoiceGroupUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceAmounts_R_ChoiceGroupId",
                table: "ImmaterialResourceAmounts",
                column: "R_ChoiceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceAmounts_ChoiceGroups_R_ChoiceGroupId",
                table: "ImmaterialResourceAmounts",
                column: "R_ChoiceGroupId",
                principalTable: "ChoiceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_ChoiceGroupUsages_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                column: "R_ChoiceGroupUsageId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceAmounts_ChoiceGroups_R_ChoiceGroupId",
                table: "ImmaterialResourceAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_CharacterId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_ChoiceGroupUsages_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropIndex(
                name: "IX_ImmaterialResourceInstances_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropIndex(
                name: "IX_ImmaterialResourceAmounts_R_ChoiceGroupId",
                table: "ImmaterialResourceAmounts");

            migrationBuilder.DropColumn(
                name: "R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropColumn(
                name: "R_ChoiceGroupId",
                table: "ImmaterialResourceAmounts");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "R_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_ImmaterialResourceInstances_CharacterId",
                table: "ImmaterialResourceInstances",
                newName: "IX_ImmaterialResourceInstances_R_CharacterId");

            migrationBuilder.CreateTable(
                name: "ClassLevelImmaterialResourceAmount",
                columns: table => new
                {
                    R_ClassLevelsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ImmaterialResourceAmountsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLevelImmaterialResourceAmount", x => new { x.R_ClassLevelsId, x.R_ImmaterialResourceAmountsId });
                    table.ForeignKey(
                        name: "FK_ClassLevelImmaterialResourceAmount_ClassLevels_R_ClassLevelsId",
                        column: x => x.R_ClassLevelsId,
                        principalTable: "ClassLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassLevelImmaterialResourceAmount_ImmaterialResourceAmounts_R_ImmaterialResourceAmountsId",
                        column: x => x.R_ImmaterialResourceAmountsId,
                        principalTable: "ImmaterialResourceAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImmaterialResourceAmountRaceLevel",
                columns: table => new
                {
                    R_ImmaterialResourceAmountsId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_RaceLevelsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmaterialResourceAmountRaceLevel", x => new { x.R_ImmaterialResourceAmountsId, x.R_RaceLevelsId });
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceAmountRaceLevel_ImmaterialResourceAmounts_R_ImmaterialResourceAmountsId",
                        column: x => x.R_ImmaterialResourceAmountsId,
                        principalTable: "ImmaterialResourceAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmaterialResourceAmountRaceLevel_RaceLevels_R_RaceLevelsId",
                        column: x => x.R_RaceLevelsId,
                        principalTable: "RaceLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevelImmaterialResourceAmount_R_ImmaterialResourceAmountsId",
                table: "ClassLevelImmaterialResourceAmount",
                column: "R_ImmaterialResourceAmountsId");

            migrationBuilder.CreateIndex(
                name: "IX_ImmaterialResourceAmountRaceLevel_R_RaceLevelsId",
                table: "ImmaterialResourceAmountRaceLevel",
                column: "R_RaceLevelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "R_CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }
    }
}
