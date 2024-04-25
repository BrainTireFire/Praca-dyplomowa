using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Identity.Migrations
{
    /// <inheritdoc />
    public partial class inheritanceItemTpc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OwnerId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objects_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objects_OwnerId",
                table: "Objects",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Objects_Id",
                table: "Items",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Objects_Id",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
