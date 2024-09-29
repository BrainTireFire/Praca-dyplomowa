using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedItemEffectInstanceRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassLevels_ImmaterialResourceInstances_ImmaterialResourceInstanceId",
                table: "ClassLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Items_R_ItemAffectedById",
                table: "EffectGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Items_R_ItemGiveEffectId",
                table: "EffectGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ResourceGrantedToItemId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectGroups_R_ItemAffectedById",
                table: "EffectGroups");

            migrationBuilder.DropIndex(
                name: "IX_EffectGroups_R_ItemGiveEffectId",
                table: "EffectGroups");

            migrationBuilder.DropIndex(
                name: "IX_ClassLevels_ImmaterialResourceInstanceId",
                table: "ClassLevels");

            migrationBuilder.DropColumn(
                name: "R_ItemAffectedById",
                table: "EffectGroups");

            migrationBuilder.DropColumn(
                name: "R_ItemGiveEffectId",
                table: "EffectGroups");

            migrationBuilder.DropColumn(
                name: "ImmaterialResourceInstanceId",
                table: "ClassLevels");

            migrationBuilder.RenameColumn(
                name: "R_ResourceGrantedToItemId",
                table: "ImmaterialResourceInstances",
                newName: "R_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ImmaterialResourceInstances_R_ResourceGrantedToItemId",
                table: "ImmaterialResourceInstances",
                newName: "IX_ImmaterialResourceInstances_R_ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "R_OwnedByGroupId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "OwnedByGroupId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "R_GrantedByEquippingItemId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_TargetedItemId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EffectGroups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_GrantedByEquippingItemId",
                table: "EffectInstances",
                column: "R_GrantedByEquippingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_R_TargetedItemId",
                table: "EffectInstances",
                column: "R_TargetedItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances",
                column: "R_OwnedByGroupId",
                principalTable: "EffectGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Items_R_GrantedByEquippingItemId",
                table: "EffectInstances",
                column: "R_GrantedByEquippingItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_Items_R_TargetedItemId",
                table: "EffectInstances",
                column: "R_TargetedItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ItemId",
                table: "ImmaterialResourceInstances",
                column: "R_ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Items_R_GrantedByEquippingItemId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_Items_R_TargetedItemId",
                table: "EffectInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ItemId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_R_GrantedByEquippingItemId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_R_TargetedItemId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "R_GrantedByEquippingItemId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "R_TargetedItemId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EffectGroups");

            migrationBuilder.RenameColumn(
                name: "R_ItemId",
                table: "ImmaterialResourceInstances",
                newName: "R_ResourceGrantedToItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ImmaterialResourceInstances_R_ItemId",
                table: "ImmaterialResourceInstances",
                newName: "IX_ImmaterialResourceInstances_R_ResourceGrantedToItemId");

            migrationBuilder.AlterColumn<int>(
                name: "R_OwnedByGroupId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnedByGroupId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_ItemAffectedById",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_ItemGiveEffectId",
                table: "EffectGroups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImmaterialResourceInstanceId",
                table: "ClassLevels",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_ItemAffectedById",
                table: "EffectGroups",
                column: "R_ItemAffectedById");

            migrationBuilder.CreateIndex(
                name: "IX_EffectGroups_R_ItemGiveEffectId",
                table: "EffectGroups",
                column: "R_ItemGiveEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLevels_ImmaterialResourceInstanceId",
                table: "ClassLevels",
                column: "ImmaterialResourceInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassLevels_ImmaterialResourceInstances_ImmaterialResourceInstanceId",
                table: "ClassLevels",
                column: "ImmaterialResourceInstanceId",
                principalTable: "ImmaterialResourceInstances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroups_Items_R_ItemAffectedById",
                table: "EffectGroups",
                column: "R_ItemAffectedById",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectGroups_Items_R_ItemGiveEffectId",
                table: "EffectGroups",
                column: "R_ItemGiveEffectId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_EffectGroups_R_OwnedByGroupId",
                table: "EffectInstances",
                column: "R_OwnedByGroupId",
                principalTable: "EffectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ResourceGrantedToItemId",
                table: "ImmaterialResourceInstances",
                column: "R_ResourceGrantedToItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
