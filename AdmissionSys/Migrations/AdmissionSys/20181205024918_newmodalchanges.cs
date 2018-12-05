using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class newmodalchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PHStatus",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "RelWithGuardian",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "StudentGuradianName",
                table: "Student",
                newName: "StudentGuardianName");

            migrationBuilder.RenameColumn(
                name: "GuradianOccupation",
                table: "Student",
                newName: "GuardianOccupation");

            migrationBuilder.AddColumn<bool>(
                name: "FillPersonalInfo",
                table: "Student",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PersonalMessage",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhysicallyHandicapStatus",
                table: "Student",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RelationWithGuardian",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Student",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElegibilityCriteria",
                table: "Programs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntakeCapacity",
                table: "Programs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Programs",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Fees",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DocumentUploaded",
                table: "Documents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Documents",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcademicRecAdded",
                table: "ApplicationList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AdmissionConfirmed",
                table: "ApplicationList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AttendInterview",
                table: "ApplicationList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmFeesPayment",
                table: "ApplicationList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CounsellingDone",
                table: "ApplicationList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FormVerified",
                table: "ApplicationList",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApplicationList",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CalcPercentage",
                table: "AcademicRecord",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MarksOrGrade",
                table: "AcademicRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObtainedOutOfOrCGPA",
                table: "AcademicRecord",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AcademicRecord",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FillPersonalInfo",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PersonalMessage",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PhysicallyHandicapStatus",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "RelationWithGuardian",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ElegibilityCriteria",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "IntakeCapacity",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "DocumentUploaded",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "AcademicRecAdded",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "AdmissionConfirmed",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "AttendInterview",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "ConfirmFeesPayment",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "CounsellingDone",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "FormVerified",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "CalcPercentage",
                table: "AcademicRecord");

            migrationBuilder.DropColumn(
                name: "MarksOrGrade",
                table: "AcademicRecord");

            migrationBuilder.DropColumn(
                name: "ObtainedOutOfOrCGPA",
                table: "AcademicRecord");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AcademicRecord");

            migrationBuilder.RenameColumn(
                name: "StudentGuardianName",
                table: "Student",
                newName: "StudentGuradianName");

            migrationBuilder.RenameColumn(
                name: "GuardianOccupation",
                table: "Student",
                newName: "GuradianOccupation");

            migrationBuilder.AddColumn<string>(
                name: "PHStatus",
                table: "Student",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelWithGuardian",
                table: "Student",
                nullable: false,
                defaultValue: "");
        }
    }
}
