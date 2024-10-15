using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class XX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "EquipDatas");

            migrationBuilder.CreateTable(
                name: "EquipDataEquipmentSlot",
                columns: table => new
                {
                    R_SlotsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsagesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipDataEquipmentSlot", x => new { x.R_SlotsId, x.UsagesId });
                    table.ForeignKey(
                        name: "FK_EquipDataEquipmentSlot_EquipDatas_UsagesId",
                        column: x => x.UsagesId,
                        principalTable: "EquipDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipDataEquipmentSlot_EquipmentSlots_R_SlotsId",
                        column: x => x.R_SlotsId,
                        principalTable: "EquipmentSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipDataEquipmentSlot_UsagesId",
                table: "EquipDataEquipmentSlot",
                column: "UsagesId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Boards",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeX",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeY",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipDataEquipmentSlot");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "EquipDatas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "SizeX",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "SizeY",
                table: "Boards");
        }
    }
}
