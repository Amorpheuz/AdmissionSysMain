using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class CreatingSchemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramsID = table.Column<string>(nullable: false),
                    ProgramName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramsID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false),
                    StudentName = table.Column<string>(maxLength: 60, nullable: false),
                    StudentAddress = table.Column<string>(maxLength: 500, nullable: false),
                    City = table.Column<string>(maxLength: 25, nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    State = table.Column<string>(maxLength: 25, nullable: false),
                    Nationality = table.Column<string>(maxLength: 25, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    StudentGuradianName = table.Column<string>(maxLength: 30, nullable: false),
                    RelWithGuardian = table.Column<string>(nullable: false),
                    BloodGroup = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    ResidencePhone = table.Column<string>(nullable: true),
                    PlaceOfBirth = table.Column<string>(maxLength: 25, nullable: false),
                    GuardianMobileNumber = table.Column<string>(nullable: false),
                    ApplicantsMobileNumber = table.Column<string>(nullable: false),
                    FamilyIncome = table.Column<decimal>(nullable: false),
                    StudentSignature = table.Column<string>(nullable: false),
                    StudentPhoto = table.Column<string>(nullable: false),
                    AadharNumber = table.Column<string>(nullable: true),
                    Caste = table.Column<int>(nullable: true),
                    GuradianOccupation = table.Column<string>(maxLength: 20, nullable: true),
                    PHStatus = table.Column<string>(nullable: false),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_IdentityUser_userID",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    AcademicYearID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AcaYear = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    IntakeCapacity = table.Column<int>(nullable: false),
                    ProgramsID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.AcademicYearID);
                    table.ForeignKey(
                        name: "FK_AcademicYear_Programs_ProgramsID",
                        column: x => x.ProgramsID,
                        principalTable: "Programs",
                        principalColumn: "ProgramsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicRecord",
                columns: table => new
                {
                    AcademicRecordID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ExamName = table.Column<string>(maxLength: 50, nullable: false),
                    BoardOrUni = table.Column<string>(maxLength: 150, nullable: false),
                    SchoolOrCollegeName = table.Column<string>(maxLength: 150, nullable: false),
                    MeritNumber = table.Column<decimal>(nullable: false),
                    MonthYearOfPass = table.Column<DateTime>(nullable: false),
                    RollNumber = table.Column<string>(maxLength: 60, nullable: false),
                    LangOfInstruction = table.Column<string>(maxLength: 60, nullable: false),
                    ScoreValidity = table.Column<DateTime>(nullable: false),
                    MarksObtained = table.Column<string>(nullable: false),
                    StudentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicRecord", x => x.AcademicRecordID);
                    table.ForeignKey(
                        name: "FK_AcademicRecord_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentsID = table.Column<string>(nullable: false),
                    DocumentPath = table.Column<string>(nullable: true),
                    StudentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentsID);
                    table.ForeignKey(
                        name: "FK_Documents_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationList",
                columns: table => new
                {
                    ApplicationListID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ApplicationCategory = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    PrioAreaOfResearch = table.Column<string>(nullable: true),
                    StudentID = table.Column<int>(nullable: true),
                    AcademicYearID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationList", x => x.ApplicationListID);
                    table.ForeignKey(
                        name: "FK_ApplicationList_AcademicYear_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYear",
                        principalColumn: "AcademicYearID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationList_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRecord_StudentID",
                table: "AcademicRecord",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYear_ProgramsID",
                table: "AcademicYear",
                column: "ProgramsID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationList_AcademicYearID",
                table: "ApplicationList",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationList_StudentID",
                table: "ApplicationList",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StudentID",
                table: "Documents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_userID",
                table: "Student",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicRecord");

            migrationBuilder.DropTable(
                name: "ApplicationList");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
