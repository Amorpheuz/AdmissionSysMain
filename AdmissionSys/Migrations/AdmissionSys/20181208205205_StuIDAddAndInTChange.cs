using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class StuIDAddAndInTChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationList_Student_StudentID",
                table: "ApplicationList");

            migrationBuilder.AlterColumn<string>(
                name: "ElegibilityCriteria",
                table: "Programs",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "ApplicationList",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationList_Student_StudentID",
                table: "ApplicationList",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationList_Student_StudentID",
                table: "ApplicationList");

            migrationBuilder.AlterColumn<int>(
                name: "ElegibilityCriteria",
                table: "Programs",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "ApplicationList",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationList_Student_StudentID",
                table: "ApplicationList",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
