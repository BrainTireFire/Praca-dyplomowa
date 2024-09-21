using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedOwnershipToNullable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_AspNetUsers_R_OwnerId",
                table: "Objects");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_AspNetUsers_R_OwnerId",
                table: "Objects",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_AspNetUsers_R_OwnerId",
                table: "Objects");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_AspNetUsers_R_OwnerId",
                table: "Objects",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
