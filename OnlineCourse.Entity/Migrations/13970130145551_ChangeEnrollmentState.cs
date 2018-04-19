using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnlineCourse.Entity.Migrations
{
    public partial class ChangeEnrollmentState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "Activity",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
