using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedOwnerStrategyToTPCRevoke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apparels_AspNetUsers_R_OwnerId",
                table: "Apparels");

            migrationBuilder.DropForeignKey(
                name: "FK_Apparels_Backpacks_R_BackpackHasItemId",
                table: "Apparels");

            migrationBuilder.DropForeignKey(
                name: "FK_Apparels_ItemFamilies_R_ItemInItemsFamilyId",
                table: "Apparels");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_AspNetUsers_R_OwnerId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_R_OwnerId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_AspNetUsers_R_OwnerId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_AspNetUsers_R_OwnerId",
                table: "Encounters");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_R_OwnerId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_AspNetUsers_R_OwnerId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Powers_AspNetUsers_R_OwnerId",
                table: "Powers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_AspNetUsers_R_OwnerId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Backpacks_R_BackpackHasItemId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ItemFamilies_R_ItemInItemsFamilyId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_AspNetUsers_R_OwnerId",
                table: "Weapons");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Backpacks_R_BackpackHasItemId",
                table: "Weapons");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_ItemFamilies_R_ItemInItemsFamilyId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_R_BackpackHasItemId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_R_ItemInItemsFamilyId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_R_OwnerId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Tools_R_BackpackHasItemId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_R_ItemInItemsFamilyId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_R_OwnerId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Powers_R_OwnerId",
                table: "Powers");

            migrationBuilder.DropIndex(
                name: "IX_Languages_R_OwnerId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Items_R_OwnerId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_R_OwnerId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_R_OwnerId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_R_OwnerId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Boards_R_OwnerId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Apparels_R_BackpackHasItemId",
                table: "Apparels");

            migrationBuilder.DropIndex(
                name: "IX_Apparels_R_ItemInItemsFamilyId",
                table: "Apparels");

            migrationBuilder.DropIndex(
                name: "IX_Apparels_R_OwnerId",
                table: "Apparels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "IsSpellFocus",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "R_BackpackHasItemId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "R_ItemInItemsFamilyId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "IsSpellFocus",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "R_BackpackHasItemId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "R_ItemInItemsFamilyId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Apparels");

            migrationBuilder.DropColumn(
                name: "IsSpellFocus",
                table: "Apparels");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Apparels");

            migrationBuilder.DropColumn(
                name: "R_BackpackHasItemId",
                table: "Apparels");

            migrationBuilder.DropColumn(
                name: "R_ItemInItemsFamilyId",
                table: "Apparels");

            migrationBuilder.DropColumn(
                name: "R_OwnerId",
                table: "Apparels");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Apparels");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Objects",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Languages",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Encounters",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Campaigns",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Apparels",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apparels_Items_Id",
                table: "Apparels",
                column: "Id",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Objects_Id",
                table: "Boards",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Objects_Id",
                table: "Campaigns",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Objects_Id",
                table: "Characters",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints",
                column: "R_CreatedByEquippingId",
                principalTable: "Items",
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
                name: "FK_Encounters_Objects_Id",
                table: "Encounters",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipDatas_Items_R_ItemId",
                table: "EquipDatas",
                column: "R_ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentSlotItem_Items_R_ItemsId",
                table: "EquipmentSlotItem",
                column: "R_ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ResourceGrantedToItemId",
                table: "ImmaterialResourceInstances",
                column: "R_ResourceGrantedToItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPower_Items_R_ItemsGrantingPowerId",
                table: "ItemPower",
                column: "R_ItemsGrantingPowerId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Objects_Id",
                table: "Items",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Objects_Id",
                table: "Languages",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Powers_Objects_Id",
                table: "Powers",
                column: "Id",
                principalTable: "Objects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Items_R_ShopHasItemId",
                table: "ShopItems",
                column: "R_ShopHasItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Items_Id",
                table: "Tools",
                column: "Id",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Items_Id",
                table: "Weapons",
                column: "Id",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apparels_Items_Id",
                table: "Apparels");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Objects_Id",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Objects_Id",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Objects_Id",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectBlueprints_Items_R_CreatedByEquippingId",
                table: "EffectBlueprints");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Items_R_ItemAffectedById",
                table: "EffectGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectGroups_Items_R_ItemGiveEffectId",
                table: "EffectGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_Objects_Id",
                table: "Encounters");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipDatas_Items_R_ItemId",
                table: "EquipDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentSlotItem_Items_R_ItemsId",
                table: "EquipmentSlotItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ImmaterialResourceInstances_Items_R_ResourceGrantedToItemId",
                table: "ImmaterialResourceInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPower_Items_R_ItemsGrantingPowerId",
                table: "ItemPower");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Objects_Id",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Objects_Id",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Powers_Objects_Id",
                table: "Powers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Items_R_ShopHasItemId",
                table: "ShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Items_Id",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Items_Id",
                table: "Weapons");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Weapons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSpellFocus",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Weapons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "R_BackpackHasItemId",
                table: "Weapons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_ItemInItemsFamilyId",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Weapons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSpellFocus",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "R_BackpackHasItemId",
                table: "Tools",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_ItemInItemsFamilyId",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Tools",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Powers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Powers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Objects",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Languages",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Languages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Encounters",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Encounters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Characters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Campaigns",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Campaigns",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Boards",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Boards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Apparels",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Apparels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSpellFocus",
                table: "Apparels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Apparels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "R_BackpackHasItemId",
                table: "Apparels",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "R_ItemInItemsFamilyId",
                table: "Apparels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "R_OwnerId",
                table: "Apparels",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Apparels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_R_BackpackHasItemId",
                table: "Weapons",
                column: "R_BackpackHasItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_R_ItemInItemsFamilyId",
                table: "Weapons",
                column: "R_ItemInItemsFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_R_OwnerId",
                table: "Weapons",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_R_BackpackHasItemId",
                table: "Tools",
                column: "R_BackpackHasItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_R_ItemInItemsFamilyId",
                table: "Tools",
                column: "R_ItemInItemsFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_R_OwnerId",
                table: "Tools",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_R_OwnerId",
                table: "Powers",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_R_OwnerId",
                table: "Languages",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_R_OwnerId",
                table: "Items",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_R_OwnerId",
                table: "Encounters",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_R_OwnerId",
                table: "Characters",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_R_OwnerId",
                table: "Campaigns",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_R_OwnerId",
                table: "Boards",
                column: "R_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Apparels_R_BackpackHasItemId",
                table: "Apparels",
                column: "R_BackpackHasItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Apparels_R_ItemInItemsFamilyId",
                table: "Apparels",
                column: "R_ItemInItemsFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Apparels_R_OwnerId",
                table: "Apparels",
                column: "R_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apparels_AspNetUsers_R_OwnerId",
                table: "Apparels",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apparels_Backpacks_R_BackpackHasItemId",
                table: "Apparels",
                column: "R_BackpackHasItemId",
                principalTable: "Backpacks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apparels_ItemFamilies_R_ItemInItemsFamilyId",
                table: "Apparels",
                column: "R_ItemInItemsFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_AspNetUsers_R_OwnerId",
                table: "Boards",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_R_OwnerId",
                table: "Campaigns",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_AspNetUsers_R_OwnerId",
                table: "Characters",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_AspNetUsers_R_OwnerId",
                table: "Encounters",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_R_OwnerId",
                table: "Items",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_AspNetUsers_R_OwnerId",
                table: "Languages",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Powers_AspNetUsers_R_OwnerId",
                table: "Powers",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_AspNetUsers_R_OwnerId",
                table: "Tools",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Backpacks_R_BackpackHasItemId",
                table: "Tools",
                column: "R_BackpackHasItemId",
                principalTable: "Backpacks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ItemFamilies_R_ItemInItemsFamilyId",
                table: "Tools",
                column: "R_ItemInItemsFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_AspNetUsers_R_OwnerId",
                table: "Weapons",
                column: "R_OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Backpacks_R_BackpackHasItemId",
                table: "Weapons",
                column: "R_BackpackHasItemId",
                principalTable: "Backpacks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_ItemFamilies_R_ItemInItemsFamilyId",
                table: "Weapons",
                column: "R_ItemInItemsFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
