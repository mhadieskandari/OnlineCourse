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
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<byte>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Os = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<string>(nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Year = table.Column<short>(nullable: false),
                    YearTerm = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessLevel = table.Column<byte>(nullable: true),
                    ActivationCode = table.Column<string>(maxLength: 20, nullable: true),
                    Addrress = table.Column<string>(maxLength: 200, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    Degree = table.Column<int>(nullable: true),
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
                    Sharj = table.Column<decimal>(nullable: false),
                    State = table.Column<byte>(nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    ValidEmail = table.Column<byte>(nullable: true),
                    ValidMobile = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: true),
                    TermId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: true),
                    HourlyPrice = table.Column<decimal>(nullable: false),
                    TeacherId = table.Column<int>(nullable: true),
                    TermId = table.Column<int>(nullable: true),
                    TotalTime = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sections_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sections_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EnrollmentId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnrollmentId = table.Column<int>(nullable: true),
                    Markdown = table.Column<decimal>(nullable: false),
                    SectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentsDetails_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollmentsDetails_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    SectionId = table.Column<int>(nullable: true),
                    StartTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TermId",
                table: "Enrollments",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentsDetails_EnrollmentId",
                table: "EnrollmentsDetails",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentsDetails_SectionId",
                table: "EnrollmentsDetails",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EnrollmentId",
                table: "Payments",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SectionId",
                table: "Schedules",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseId",
                table: "Sections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_TeacherId",
                table: "Sections",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_TermId",
                table: "Sections",
                column: "TermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentsDetails");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Terms");
        }
    }
}
