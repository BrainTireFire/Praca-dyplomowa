using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pracadyplomowa.Data.Migrations
{
    /// <inheritdoc />
    public partial class hitpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionLogs_Campaigns_CampaignId",
                table: "ActionLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActionLogs_CampaignId",
                table: "ActionLogs");

            migrationBuilder.DropColumn(
                name: "R_CharacterOccupiesFieldId",
                table: "ParticipanceDatas");

            migrationBuilder.DropColumn(
                name: "R_EncounterInTheCampaignId",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "ActionLogs");

            migrationBuilder.RenameColumn(
                name: "Light",
                table: "Weapons",
                newName: "WeaponWeight");

            migrationBuilder.RenameColumn(
                name: "Heavy",
                table: "Weapons",
                newName: "Range");

            migrationBuilder.RenameColumn(
                name: "R_EncounterId",
                table: "ActionLogs",
                newName: "R_CampaignId");

            migrationBuilder.AddColumn<bool>(
                name: "LoadedRange",
                table: "Weapons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FieldCoverLevel",
                table: "Fields",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemFamilyId",
                table: "EffectInstances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hitpoints",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Campaigns",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvitationLink",
                table: "Campaigns",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EffectInstances_ItemFamilyId",
                table: "EffectInstances",
                column: "ItemFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_R_CampaignId",
                table: "ActionLogs",
                column: "R_CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionLogs_Campaigns_R_CampaignId",
                table: "ActionLogs",
                column: "R_CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EffectInstances_ItemFamilies_ItemFamilyId",
                table: "EffectInstances",
                column: "ItemFamilyId",
                principalTable: "ItemFamilies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionLogs_Campaigns_R_CampaignId",
                table: "ActionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_EffectInstances_ItemFamilies_ItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_EffectInstances_ItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropIndex(
                name: "IX_ActionLogs_R_CampaignId",
                table: "ActionLogs");

            migrationBuilder.DropColumn(
                name: "LoadedRange",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "FieldCoverLevel",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ItemFamilyId",
                table: "EffectInstances");

            migrationBuilder.DropColumn(
                name: "Hitpoints",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "InvitationLink",
                table: "Campaigns");

            migrationBuilder.RenameColumn(
                name: "WeaponWeight",
                table: "Weapons",
                newName: "Light");

            migrationBuilder.RenameColumn(
                name: "Range",
                table: "Weapons",
                newName: "Heavy");

            migrationBuilder.RenameColumn(
                name: "R_CampaignId",
                table: "ActionLogs",
                newName: "R_EncounterId");

            migrationBuilder.AddColumn<int>(
                name: "R_CharacterOccupiesFieldId",
                table: "ParticipanceDatas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "R_EncounterInTheCampaignId",
                table: "Encounters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "ActionLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_CampaignId",
                table: "ActionLogs",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionLogs_Campaigns_CampaignId",
                table: "ActionLogs",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
