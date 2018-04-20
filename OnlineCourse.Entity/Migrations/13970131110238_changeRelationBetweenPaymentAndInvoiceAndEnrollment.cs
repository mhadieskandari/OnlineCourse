using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OnlineCourse.Entity.Migrations
{
    public partial class changeRelationBetweenPaymentAndInvoiceAndEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Invoices_InvoiceId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_InvoiceId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Invoices_InvoiceId",
                table: "Payments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_InvoiceId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_InvoiceId",
                table: "Enrollments",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Invoices_InvoiceId",
                table: "Enrollments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
