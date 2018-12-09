using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class docuploadtran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentUploaded",
                table: "Documents");

            migrationBuilder.AddColumn<bool>(
                name: "DocumentUploaded",
                table: "ApplicationList",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentUploaded",
                table: "ApplicationList");

            migrationBuilder.AddColumn<bool>(
                name: "DocumentUploaded",
                table: "Documents",
                nullable: false,
                defaultValue: false);
        }
    }
}
