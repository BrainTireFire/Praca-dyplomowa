using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeCharacterToResourceInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "R_CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Characters_R_CharacterId",
                table: "ImmaterialResourceInstances",
                column: "R_CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }
    }
}
