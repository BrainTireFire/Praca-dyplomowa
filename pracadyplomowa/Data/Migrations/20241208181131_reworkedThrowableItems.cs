using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class reworkedThrowableItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeleeThrowableWeapons");

            migrationBuilder.RenameColumn(
                name: "Range",
                table: "RangedWeapons",
                newName: "Loaded");

            migrationBuilder.RenameColumn(
                name: "LoadedRange",
                table: "RangedWeapons",
                newName: "IsReloaded");

            migrationBuilder.AddColumn<int>(
                name: "Range",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Thrown",
                table: "MeleeWeapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Range",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Thrown",
                table: "MeleeWeapons");

            migrationBuilder.RenameColumn(
                name: "Loaded",
                table: "RangedWeapons",
                newName: "Range");

            migrationBuilder.RenameColumn(
                name: "IsReloaded",
                table: "RangedWeapons",
                newName: "LoadedRange");

            migrationBuilder.CreateTable(
                name: "MeleeThrowableWeapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Range = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeleeThrowableWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeleeThrowableWeapons_MeleeWeapons_Id",
                        column: x => x.Id,
                        principalTable: "MeleeWeapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
