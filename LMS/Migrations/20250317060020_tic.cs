using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Migrations
{
    /// <inheritdoc />
    public partial class tic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Courses_CourseId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_CourseId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Lecturers");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Subjects",
                newName: "AssessmentId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Subjects_SubjectId",
                table: "Courses",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Subjects_SubjectId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "AssessmentId",
                table: "Subjects",
                newName: "CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CourseId",
                table: "Subjects",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Courses_CourseId",
                table: "Subjects",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
