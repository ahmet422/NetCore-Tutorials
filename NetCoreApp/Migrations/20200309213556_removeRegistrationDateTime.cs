using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreApp.Migrations
{
    public partial class removeRegistrationDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfRegistration",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRegistration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
