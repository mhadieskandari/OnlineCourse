using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnlineCourse.Entity.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Users",
                newName: "BirthDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "Sharj",
                table: "Users",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Users",
                newName: "Birthday");

            migrationBuilder.AlterColumn<decimal>(
                name: "Sharj",
                table: "Users",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
