using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class percenupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalcPercentage",
                table: "AcademicRecord");

            migrationBuilder.AlterColumn<string>(
                name: "ObtainedOutOfOrCGPA",
                table: "AcademicRecord",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MarksObtained",
                table: "AcademicRecord",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CalcPer",
                table: "AcademicRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalcPer",
                table: "AcademicRecord");

            migrationBuilder.AlterColumn<string>(
                name: "ObtainedOutOfOrCGPA",
                table: "AcademicRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarksObtained",
                table: "AcademicRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CalcPercentage",
                table: "AcademicRecord",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
