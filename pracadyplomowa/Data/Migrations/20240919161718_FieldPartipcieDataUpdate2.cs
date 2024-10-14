using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class FieldPartipcieDataUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields");

            migrationBuilder.AlterColumn<int>(
                name: "R_OccupiedById",
                table: "Fields",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields",
                column: "R_OccupiedById",
                principalTable: "ParticipanceDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields");

            migrationBuilder.AlterColumn<int>(
                name: "R_OccupiedById",
                table: "Fields",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_ParticipanceDatas_R_OccupiedById",
                table: "Fields",
                column: "R_OccupiedById",
                principalTable: "ParticipanceDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
