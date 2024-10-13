using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class X : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finesse",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "LoadedRange",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Range",
                table: "Weapons");

            migrationBuilder.CreateTable(
                name: "MeleeWeapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Finesse = table.Column<bool>(type: "INTEGER", nullable: false),
                    Reach = table.Column<bool>(type: "INTEGER", nullable: false),
                    Versatile = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeleeWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeleeWeapons_Weapons_Id",
                        column: x => x.Id,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RangedWeapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Range = table.Column<int>(type: "INTEGER", nullable: false),
                    LoadedRange = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangedWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangedWeapons_Weapons_Id",
                        column: x => x.Id,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeleeThrowableWeapons");

            migrationBuilder.DropTable(
                name: "RangedWeapons");

            migrationBuilder.DropTable(
                name: "MeleeWeapons");

            migrationBuilder.AddColumn<bool>(
                name: "Finesse",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LoadedRange",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Range",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
