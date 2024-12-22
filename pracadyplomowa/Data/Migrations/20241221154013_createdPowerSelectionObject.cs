using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class createdPowerSelectionObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "CharacterPower1");

            migrationBuilder.AlterColumn<int>(
                name: "MaximumPreparedSpellsFormulaId",
                table: "Classes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PowerSelections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    R_ClassId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_CharacterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSelections_Characters_R_CharacterId",
                        column: x => x.R_CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerSelections_Classes_R_ClassId",
                        column: x => x.R_ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerPowerSelection",
                columns: table => new
                {
                    R_CharacterPreparedPowersId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_PreparedPowersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPowerSelection", x => new { x.R_CharacterPreparedPowersId, x.R_PreparedPowersId });
                    table.ForeignKey(
                        name: "FK_PowerPowerSelection_PowerSelections_R_CharacterPreparedPowersId",
                        column: x => x.R_CharacterPreparedPowersId,
                        principalTable: "PowerSelections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerPowerSelection_Powers_R_PreparedPowersId",
                        column: x => x.R_PreparedPowersId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PowerPowerSelection_R_PreparedPowersId",
                table: "PowerPowerSelection",
                column: "R_PreparedPowersId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSelections_R_CharacterId",
                table: "PowerSelections",
                column: "R_CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSelections_R_ClassId",
                table: "PowerSelections",
                column: "R_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId",
                principalTable: "DiceSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "PowerPowerSelection");

            migrationBuilder.DropTable(
                name: "PowerSelections");

            migrationBuilder.AlterColumn<int>(
                name: "MaximumPreparedSpellsFormulaId",
                table: "Classes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "CharacterPower1",
                columns: table => new
                {
                    R_CharacterPreparedPowersId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_PowersPreparedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterPower1", x => new { x.R_CharacterPreparedPowersId, x.R_PowersPreparedId });
                    table.ForeignKey(
                        name: "FK_CharacterPower1_Characters_R_CharacterPreparedPowersId",
                        column: x => x.R_CharacterPreparedPowersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterPower1_Powers_R_PowersPreparedId",
                        column: x => x.R_PowersPreparedId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterPower1_R_PowersPreparedId",
                table: "CharacterPower1",
                column: "R_PowersPreparedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_DiceSet_MaximumPreparedSpellsFormulaId",
                table: "Classes",
                column: "MaximumPreparedSpellsFormulaId",
                principalTable: "DiceSet",
                principalColumn: "Id");
        }
    }
}
