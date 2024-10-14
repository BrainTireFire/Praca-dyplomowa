using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class introducedLanguageEffect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEffectGroup");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);

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
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_Objects_Id",
                        column: x => x.Id,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_LanguageId",
                table: "EffectInstances",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectBlueprints_LanguageId",
                table: "EffectBlueprints",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEffectInstance_R_TargetedCharactersId",
                table: "CharacterEffectInstance",
                column: "R_TargetedCharactersId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Languages_LanguageId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Languages_LanguageId",
                table: "EffectInstances");

            migrationBuilder.DropTable(
                name: "CharacterEffectInstance");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_LanguageId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectBlueprints_LanguageId",
                table: "EffectBlueprints");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "EffectBlueprints");

            migrationBuilder.CreateTable(
                name: "CharacterEffectGroup",
                columns: table => new
                {
                    R_AffectedById = table.Column<int>(type: "INTEGER", nullable: false),
                    R_TargetedCharactersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEffectGroup", x => new { x.R_AffectedById, x.R_TargetedCharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterEffectGroup_Characters_R_TargetedCharactersId",
                        column: x => x.R_TargetedCharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEffectGroup_EffectGroups_R_AffectedById",
                        column: x => x.R_AffectedById,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEffectGroup_R_TargetedCharactersId",
                table: "CharacterEffectGroup",
                column: "R_TargetedCharactersId");
        }
    }
}
