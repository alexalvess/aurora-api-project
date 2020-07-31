using Microsoft.EntityFrameworkCore.Migrations;

namespace Aurora.Infra.Data.Migrations
{
    public partial class CreateRelationalEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonalProtectiveEquipmentId",
                table: "PersonalProtectiveEquipmentPossession",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "PersonalProtectiveEquipmentPossession",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalProtectiveEquipmentPossession_PersonalProtectiveEqui~",
                table: "PersonalProtectiveEquipmentPossession",
                column: "PersonalProtectiveEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalProtectiveEquipmentPossession_WorkerId",
                table: "PersonalProtectiveEquipmentPossession",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalProtectiveEquipmentPossession_PersonalProtectiveEqui~",
                table: "PersonalProtectiveEquipmentPossession",
                column: "PersonalProtectiveEquipmentId",
                principalTable: "PersonalProtectiveEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalProtectiveEquipmentPossession_Worker_WorkerId",
                table: "PersonalProtectiveEquipmentPossession",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalProtectiveEquipmentPossession_PersonalProtectiveEqui~",
                table: "PersonalProtectiveEquipmentPossession");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalProtectiveEquipmentPossession_Worker_WorkerId",
                table: "PersonalProtectiveEquipmentPossession");

            migrationBuilder.DropIndex(
                name: "IX_PersonalProtectiveEquipmentPossession_PersonalProtectiveEqui~",
                table: "PersonalProtectiveEquipmentPossession");

            migrationBuilder.DropIndex(
                name: "IX_PersonalProtectiveEquipmentPossession_WorkerId",
                table: "PersonalProtectiveEquipmentPossession");

            migrationBuilder.DropColumn(
                name: "PersonalProtectiveEquipmentId",
                table: "PersonalProtectiveEquipmentPossession");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "PersonalProtectiveEquipmentPossession");
        }
    }
}
