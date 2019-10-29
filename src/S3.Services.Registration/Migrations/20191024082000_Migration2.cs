using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace S3.Services.Registration.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Schools",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Schools",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdministratorId",
                table: "Schools",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_AdministratorId",
                table: "Schools",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Teachers_AdministratorId",
                table: "Schools",
                column: "AdministratorId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Teachers_AdministratorId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_AdministratorId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Schools");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Schools",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Schools",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
