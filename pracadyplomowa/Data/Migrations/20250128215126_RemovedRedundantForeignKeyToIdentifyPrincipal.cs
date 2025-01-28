using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRedundantForeignKeyToIdentifyPrincipal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "R_OccupiedFieldId",
                table: "ParticipanceDatas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "R_OccupiedFieldId",
                table: "ParticipanceDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
