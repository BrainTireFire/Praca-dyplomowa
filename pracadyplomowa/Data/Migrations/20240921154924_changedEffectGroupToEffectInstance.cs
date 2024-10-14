using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedEffectGroupToEffectInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoiceGroupUsageEffectGroup");

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
                name: "IX_ChoiceGroupUsageEffectInstance_R_GrantedThroughId",
                table: "ChoiceGroupUsageEffectInstance",
                column: "R_GrantedThroughId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoiceGroupUsageEffectInstance");

            migrationBuilder.CreateTable(
                name: "ChoiceGroupUsageEffectGroup",
                columns: table => new
                {
                    R_EffectGroupsGrantedId = table.Column<int>(type: "INTEGER", nullable: false),
                    R_GrantedThroughId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceGroupUsageEffectGroup", x => new { x.R_EffectGroupsGrantedId, x.R_GrantedThroughId });
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsageEffectGroup_ChoiceGroupUsages_R_GrantedThroughId",
                        column: x => x.R_GrantedThroughId,
                        principalTable: "ChoiceGroupUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceGroupUsageEffectGroup_EffectGroups_R_EffectGroupsGrantedId",
                        column: x => x.R_EffectGroupsGrantedId,
                        principalTable: "EffectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceGroupUsageEffectGroup_R_GrantedThroughId",
                table: "ChoiceGroupUsageEffectGroup",
                column: "R_GrantedThroughId");
        }
    }
}
