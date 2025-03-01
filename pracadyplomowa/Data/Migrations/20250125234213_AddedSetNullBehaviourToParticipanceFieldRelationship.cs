using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSetNullBehaviourToParticipanceFieldRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields",
                column: "R_OccupiedById",
                principalTable: "ParticipanceDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields",
                column: "R_OccupiedById",
                principalTable: "ParticipanceDatas",
                principalColumn: "Id");
        }
    }
}
