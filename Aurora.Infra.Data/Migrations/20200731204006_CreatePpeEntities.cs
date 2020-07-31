using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aurora.Infra.Data.Migrations
{
    public partial class CreatePpeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalProtectiveEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ApprovalCertificate = table.Column<string>(type: "varchar(50)", nullable: false),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    Durability = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalProtectiveEquipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalProtectiveEquipmentPossession",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    Confirmation = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalProtectiveEquipmentPossession", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalProtectiveEquipment");

            migrationBuilder.DropTable(
                name: "PersonalProtectiveEquipmentPossession");
        }
    }
}
