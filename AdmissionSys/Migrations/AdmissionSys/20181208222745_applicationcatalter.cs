using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSys.Migrations.AdmissionSys
{
    public partial class applicationcatalter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationCategory",
                table: "ApplicationList");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ApplicationList",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ApplicationCat",
                table: "ApplicationList",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationCat",
                table: "ApplicationList");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ApplicationList",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationCategory",
                table: "ApplicationList",
                nullable: false,
                defaultValue: "");
        }
    }
}
