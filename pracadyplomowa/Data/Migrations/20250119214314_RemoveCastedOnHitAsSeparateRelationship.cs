using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCastedOnHitAsSeparateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances");

            migrationBuilder.DropTable(
                name: "PowerWeapon");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances",
                column: "R_OwnedByGroupId",
                principalTable: "EffectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances");

            migrationBuilder.CreateTable(
                name: "PowerWeapon",
                columns: table => new
                {
                    R_PowersCastedOnHitId = table.Column<int>(type: "integer", nullable: false),
                    R_WeaponsCastingOnHitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerWeapon", x => new { x.R_PowersCastedOnHitId, x.R_WeaponsCastingOnHitId });
                    table.ForeignKey(
                        name: "FK_PowerWeapon_Powers_R_PowersCastedOnHitId",
                        column: x => x.R_PowersCastedOnHitId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerWeapon_Weapons_R_WeaponsCastingOnHitId",
                        column: x => x.R_WeaponsCastingOnHitId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PowerWeapon_R_WeaponsCastingOnHitId",
                table: "PowerWeapon",
                column: "R_WeaponsCastingOnHitId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances",
                column: "R_OwnedByGroupId",
                principalTable: "EffectGroups",
                principalColumn: "Id");
        }
    }
}
