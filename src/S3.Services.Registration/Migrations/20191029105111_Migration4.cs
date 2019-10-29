
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Registration.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "ScoresEntryTasks",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "ScoresEntryTasks",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "ScoresEntryTasks");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "ScoresEntryTasks");
        }
    }
}
