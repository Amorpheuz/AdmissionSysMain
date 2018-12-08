using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class altdatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AcademicRecord");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOpr",
                table: "Student",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Programs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOpr",
                table: "Programs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Programs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOpr",
                table: "Fees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOpr",
                table: "Documents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOpr",
                table: "ApplicationList",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOpr",
                table: "AcademicRecord",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastOpr",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "LastOpr",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "LastOpr",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "LastOpr",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "LastOpr",
                table: "ApplicationList");

            migrationBuilder.DropColumn(
                name: "LastOpr",
                table: "AcademicRecord");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Student",
                rowVersion: true,
                nullable: true);

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

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Documents",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ApplicationList",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AcademicRecord",
                rowVersion: true,
                nullable: true);
        }
    }
}
