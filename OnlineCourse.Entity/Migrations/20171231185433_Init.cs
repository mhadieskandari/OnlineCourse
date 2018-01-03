using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnlineCourse.Entity.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessLevel = table.Column<byte>(nullable: true),
                    ActivationCode = table.Column<string>(maxLength: 20, nullable: true),
                    Addrress = table.Column<string>(maxLength: 200, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FullName = table.Column<string>(maxLength: 20, nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastLoginIp = table.Column<string>(maxLength: 20, nullable: true),
                    LastRequestActivationCode = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastResetPasswordDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastVisitedNewsId = table.Column<int>(nullable: true),
                    LoginAttemptFailure = table.Column<int>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 50, nullable: true),
                    MobileCreditAlarm = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Position = table.Column<string>(maxLength: 30, nullable: true),
                    RegisterAttemptFailure = table.Column<int>(nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SecuritySpan = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    ValidEmail = table.Column<byte>(nullable: true),
                    ValidMobile = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
