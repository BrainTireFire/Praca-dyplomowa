using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class separatedKnownFromPrepared : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupPower_ChoiceGroups_R_ChoiceGroupsId",
                table: "ChoiceGroupPower");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupPower_Powers_R_PowersId",
                table: "ChoiceGroupPower");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupUsagePower_ChoiceGroupUsages_R_GrantedThroughId",
                table: "ChoiceGroupUsagePower");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupUsagePower_Powers_R_PowersGrantedId",
                table: "ChoiceGroupUsagePower");

            migrationBuilder.RenameColumn(
                name: "R_PowersGrantedId",
                table: "ChoiceGroupUsagePower",
                newName: "R_PowersAlwaysAvailableGrantedId");

            migrationBuilder.RenameColumn(
                name: "R_GrantedThroughId",
                table: "ChoiceGroupUsagePower",
                newName: "R_AlwaysAvailableThroughChoiceGroupUsageId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoiceGroupUsagePower_R_PowersGrantedId",
                table: "ChoiceGroupUsagePower",
                newName: "IX_ChoiceGroupUsagePower_R_PowersAlwaysAvailableGrantedId");

            migrationBuilder.RenameColumn(
                name: "R_PowersId",
                table: "ChoiceGroupPower",
                newName: "R_PowersAlwaysAvailableId");

            migrationBuilder.RenameColumn(
                name: "R_ChoiceGroupsId",
                table: "ChoiceGroupPower",
                newName: "R_AlwaysAvailableThroughChoiceGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoiceGroupPower_R_PowersId",
                table: "ChoiceGroupPower",
                newName: "IX_ChoiceGroupPower_R_PowersAlwaysAvailableId");

            migrationBuilder.CreateTable(
                name: "ChoiceGroupPower1",
                columns: table => new
                {
                    R_PowersToPrepareId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ToPrepareThroughChoiceGroupsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupPower1", x => new { x.R_PowersToPrepareId, x.R_ToPrepareThroughChoiceGroupsId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupPower1_ChoiceGroups_R_ToPrepareThroughChoiceGroupsId",
                        column: x => x.R_ToPrepareThroughChoiceGroupsId,
                        principalTable: "ChoiceGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupPower1_Powers_R_PowersToPrepareId",
                        column: x => x.R_PowersToPrepareId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsagePower1",
                columns: table => new
                {
                    R_PowersToPrepareGrantedId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_ToPrepareThroughChoiceGroupUsageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsagePower1", x => new { x.R_PowersToPrepareGrantedId, x.R_ToPrepareThroughChoiceGroupUsageId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsagePower1_ChoiceGroupUsages_R_ToPrepareThroughChoiceGroupUsageId",
                        column: x => x.R_ToPrepareThroughChoiceGroupUsageId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsagePower1_Powers_R_PowersToPrepareGrantedId",
                        column: x => x.R_PowersToPrepareGrantedId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupPower1_R_ToPrepareThroughChoiceGroupsId",
                table: "ChoiceGroupPower1",
                column: "R_ToPrepareThroughChoiceGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsagePower1_R_ToPrepareThroughChoiceGroupUsageId",
                table: "ChoiceGroupUsagePower1",
                column: "R_ToPrepareThroughChoiceGroupUsageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower_ChoiceGroups_R_AlwaysAvailableThroughChoiceGroupId",
                table: "ChoiceGroupPower",
                column: "R_AlwaysAvailableThroughChoiceGroupId",
                principalTable: "ChoiceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower_Powers_R_PowersAlwaysAvailableId",
                table: "ChoiceGroupPower",
                column: "R_PowersAlwaysAvailableId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupUsagePower_ChoiceGroupUsages_R_AlwaysAvailableThroughChoiceGroupUsageId",
                table: "ChoiceGroupUsagePower",
                column: "R_AlwaysAvailableThroughChoiceGroupUsageId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupUsagePower_Powers_R_PowersAlwaysAvailableGrantedId",
                table: "ChoiceGroupUsagePower",
                column: "R_PowersAlwaysAvailableGrantedId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupPower_ChoiceGroups_R_AlwaysAvailableThroughChoiceGroupId",
                table: "ChoiceGroupPower");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupPower_Powers_R_PowersAlwaysAvailableId",
                table: "ChoiceGroupPower");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupUsagePower_ChoiceGroupUsages_R_AlwaysAvailableThroughChoiceGroupUsageId",
                table: "ChoiceGroupUsagePower");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoiceGroupUsagePower_Powers_R_PowersAlwaysAvailableGrantedId",
                table: "ChoiceGroupUsagePower");

            migrationBuilder.DropTable(
                name: "ChoiceGroupPower1");

            migrationBuilder.DropTable(
                name: "ChoiceGroupUsagePower1");

            migrationBuilder.RenameColumn(
                name: "R_PowersAlwaysAvailableGrantedId",
                table: "ChoiceGroupUsagePower",
                newName: "R_PowersGrantedId");

            migrationBuilder.RenameColumn(
                name: "R_AlwaysAvailableThroughChoiceGroupUsageId",
                table: "ChoiceGroupUsagePower",
                newName: "R_GrantedThroughId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoiceGroupUsagePower_R_PowersAlwaysAvailableGrantedId",
                table: "ChoiceGroupUsagePower",
                newName: "IX_ChoiceGroupUsagePower_R_PowersGrantedId");

            migrationBuilder.RenameColumn(
                name: "R_PowersAlwaysAvailableId",
                table: "ChoiceGroupPower",
                newName: "R_PowersId");

            migrationBuilder.RenameColumn(
                name: "R_AlwaysAvailableThroughChoiceGroupId",
                table: "ChoiceGroupPower",
                newName: "R_ChoiceGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoiceGroupPower_R_PowersAlwaysAvailableId",
                table: "ChoiceGroupPower",
                newName: "IX_ChoiceGroupPower_R_PowersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower_ChoiceGroups_R_ChoiceGroupsId",
                table: "ChoiceGroupPower",
                column: "R_ChoiceGroupsId",
                principalTable: "ChoiceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupPower_Powers_R_PowersId",
                table: "ChoiceGroupPower",
                column: "R_PowersId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupUsagePower_ChoiceGroupUsages_R_GrantedThroughId",
                table: "ChoiceGroupUsagePower",
                column: "R_GrantedThroughId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoiceGroupUsagePower_Powers_R_PowersGrantedId",
                table: "ChoiceGroupUsagePower",
                column: "R_PowersGrantedId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
