using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Registration.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RuleId",
                table: "ScoresEntryTasks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuleId",
                table: "ScoresEntryTasks");
        }
    }
}
