using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class ACPCNameofDegree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ACPCMeritNumber",
                table: "AcademicRecord",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfDegree",
                table: "AcademicRecord",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecializationOrMainSubject",
                table: "AcademicRecord",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACPCMeritNumber",
                table: "AcademicRecord");

            migrationBuilder.DropColumn(
                name: "NameOfDegree",
                table: "AcademicRecord");

            migrationBuilder.DropColumn(
                name: "SpecializationOrMainSubject",
                table: "AcademicRecord");
        }
    }
}
