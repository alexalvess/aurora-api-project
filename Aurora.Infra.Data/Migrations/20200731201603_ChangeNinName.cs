using Microsoft.EntityFrameworkCore.Migrations;

namespace Aurora.Infra.Data.Migrations
{
    public partial class ChangeNinName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nin",
                table: "Worker",
                newName: "NationalInsuranceNumber");

            migrationBuilder.RenameColumn(
                name: "Nin",
                table: "Manager",
                newName: "NationalInsuranceNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NationalInsuranceNumber",
                table: "Worker",
                newName: "Nin");

            migrationBuilder.RenameColumn(
                name: "NationalInsuranceNumber",
                table: "Manager",
                newName: "Nin");
        }
    }
}
