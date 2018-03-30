using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnlineCourse.Entity.Migrations
{
    public partial class changeStateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Users",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "State",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
