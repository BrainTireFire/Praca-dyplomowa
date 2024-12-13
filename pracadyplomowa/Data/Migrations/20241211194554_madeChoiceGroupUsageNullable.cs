using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class madeChoiceGroupUsageNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_ChoiceGroupUsages_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.AlterColumn<int>(
                name: "R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_ChoiceGroupUsages_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                column: "R_ChoiceGroupUsageId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_ChoiceGroupUsages_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.AlterColumn<int>(
                name: "R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_ChoiceGroupUsages_R_ChoiceGroupUsageId",
                table: "ImmaterialResourceInstances",
                column: "R_ChoiceGroupUsageId",
                principalTable: "ChoiceGroupUsages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
