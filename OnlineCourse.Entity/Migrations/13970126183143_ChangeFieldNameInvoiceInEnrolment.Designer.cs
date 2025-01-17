﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OnlineCourse.Entity;
using System;

namespace OnlineCourse.Entity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("13970126183143_ChangeFieldNameInvoiceInEnrolment")]
    partial class ChangeFieldNameInvoiceInEnrolment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineCourse.Entity.Models.ClassRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte?>("ChangeTimePermit");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<TimeSpan>("EndedTime");

                    b.Property<int>("PresentId");

                    b.Property<string>("Source");

                    b.Property<TimeSpan>("StartedTime");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("PresentId");

                    b.ToTable("ClassRooms");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.ClassRoomDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClassRoomId");

                    b.Property<int>("Kind");

                    b.Property<string>("Source");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("ClassRoomId");

                    b.HasIndex("StudentId");

                    b.ToTable("ClassRoomDetails");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Activity");

                    b.Property<int>("InvoiceId");

                    b.Property<decimal>("Markdown");

                    b.Property<int>("PresentId");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PresentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Gallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Des")
                        .HasMaxLength(400);

                    b.Property<string>("Ext")
                        .HasMaxLength(4);

                    b.Property<byte?>("Kind");

                    b.Property<byte?>("POrder");

                    b.Property<int>("PublicId");

                    b.Property<byte?>("State");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte?>("Action");

                    b.Property<string>("Browser");

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Ip");

                    b.Property<string>("Message");

                    b.Property<string>("Os");

                    b.Property<byte?>("State");

                    b.Property<string>("Url");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BankId");

                    b.Property<string>("Code")
                        .HasMaxLength(50);

                    b.Property<string>("Ip")
                        .HasMaxLength(20);

                    b.Property<DateTime>("LastModifieDateTime");

                    b.Property<int>("PayState");

                    b.Property<int>("PayType");

                    b.Property<int>("State");

                    b.Property<string>("TransactionId");

                    b.HasKey("Id");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int>("EnrollmentId");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Present", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SectionId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Presents");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<TimeSpan>("EndTime");

                    b.Property<int>("PresentId");

                    b.Property<TimeSpan>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("PresentId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Activity");

                    b.Property<int>("CourseId");

                    b.Property<decimal>("HourlyPrice");

                    b.Property<int>("TeacherId");

                    b.Property<int>("TermId");

                    b.Property<decimal>("TotalTime");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("TermId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("EndDate")
                        .IsRequired();

                    b.Property<string>("StartDate")
                        .IsRequired();

                    b.Property<int>("State");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<short>("Year");

                    b.Property<short>("YearTerm");

                    b.HasKey("Id");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessLevel");

                    b.Property<string>("ActivationCode")
                        .HasMaxLength(20);

                    b.Property<string>("Addrress")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<int?>("Degree");

                    b.Property<string>("Description")
                        .HasMaxLength(400);

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FullName")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LastLoginIp")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("LastRequestActivationCode")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastResetPasswordDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("LastVisitedNewsId");

                    b.Property<int?>("LoginAttemptFailure");

                    b.Property<string>("Mobile")
                        .HasMaxLength(50);

                    b.Property<string>("MobileCreditAlarm")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .HasMaxLength(200);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<int?>("RegisterAttemptFailure");

                    b.Property<DateTime?>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<string>("SecuritySpan");

                    b.Property<int>("State");

                    b.Property<string>("UserName")
                        .HasMaxLength(100);

                    b.Property<byte?>("ValidEmail");

                    b.Property<byte?>("ValidMobile");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.ClassRoom", b =>
                {
                    b.HasOne("OnlineCourse.Entity.Models.Present", "Present")
                        .WithMany("ClassRooms")
                        .HasForeignKey("PresentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.ClassRoomDetails", b =>
                {
                    b.HasOne("OnlineCourse.Entity.Models.ClassRoom", "ClassRoom")
                        .WithMany()
                        .HasForeignKey("ClassRoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineCourse.Entity.Models.User", "Student")
                        .WithMany("ClassRoomDetails")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Enrollment", b =>
                {
                    b.HasOne("OnlineCourse.Entity.Models.Invoice", "Invoice")
                        .WithMany("Enrollments")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineCourse.Entity.Models.Present", "Present")
                        .WithMany("Enrollments")
                        .HasForeignKey("PresentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OnlineCourse.Entity.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Payment", b =>
                {
                    b.HasOne("OnlineCourse.Entity.Models.Enrollment", "Enrollment")
                        .WithMany("Payments")
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Present", b =>
                {
                    b.HasOne("OnlineCourse.Entity.Models.Section", "Section")
                        .WithMany("Presents")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Schedule", b =>
                {
                    b.HasOne("OnlineCourse.Entity.Models.Present", "Present")
                        .WithMany("Schedules")
                        .HasForeignKey("PresentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineCourse.Entity.Models.Section", b =>
                {
                    b.HasOne("OnlineCourse.Entity.Models.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineCourse.Entity.Models.User", "Teacher")
                        .WithMany("Sections")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineCourse.Entity.Models.Term", "Term")
                        .WithMany("Sections")
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
