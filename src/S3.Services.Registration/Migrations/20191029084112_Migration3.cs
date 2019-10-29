using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Registration.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "ScoresEntryTasks",
                newName: "SchoolId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ScoresEntryTasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "ScoresEntryTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ScoresEntryTasks",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ScoresEntryTasks_ClassId",
                table: "ScoresEntryTasks",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoresEntryTasks_Classes_ClassId",
                table: "ScoresEntryTasks",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoresEntryTasks_Classes_ClassId",
                table: "ScoresEntryTasks");

            migrationBuilder.DropIndex(
                name: "IX_ScoresEntryTasks_ClassId",
                table: "ScoresEntryTasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ScoresEntryTasks");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "ScoresEntryTasks");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ScoresEntryTasks");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "ScoresEntryTasks",
                newName: "SubjectId");
        }
    }
}
