using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class inheritanceInEffectInstance2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrantsProficiencyInItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrantsProficiencyInItemFamilyId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GrantsProficiencyInItemFamilyId",
                table: "EffectBlueprints",
                type: "INTEGER",
                nullable: true);
        }
    }
}
