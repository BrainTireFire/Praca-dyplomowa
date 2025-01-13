using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeParticipinceDataFieldsEncoutnerIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fields_R_OccupiedById",
                table: "Fields");

            migrationBuilder.AddColumn<int>(
                name: "R_OccupiedFieldId",
                table: "ParticipanceDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Encounters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_R_OccupiedById",
                table: "Fields",
                column: "R_OccupiedById",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fields_R_OccupiedById",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "R_OccupiedFieldId",
                table: "ParticipanceDatas");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Encounters");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_R_OccupiedById",
                table: "Fields",
                column: "R_OccupiedById");
        }
    }
}
