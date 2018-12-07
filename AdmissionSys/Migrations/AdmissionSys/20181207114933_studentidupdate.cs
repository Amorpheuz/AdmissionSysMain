using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class studentidupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicRecord_Student_StudentID",
                table: "AcademicRecord");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "AcademicRecord",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicRecord_Student_StudentID",
                table: "AcademicRecord",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicRecord_Student_StudentID",
                table: "AcademicRecord");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "AcademicRecord",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicRecord_Student_StudentID",
                table: "AcademicRecord",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
