using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Batchs_BatchId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Batchs",
                table: "Batchs");

            migrationBuilder.RenameTable(
                name: "Batchs",
                newName: "Batches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Batches",
                table: "Batches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Batches_BatchId",
                table: "Courses",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Batches_BatchId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Batches",
                table: "Batches");

            migrationBuilder.RenameTable(
                name: "Batches",
                newName: "Batchs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Batchs",
                table: "Batchs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Batchs_BatchId",
                table: "Courses",
                column: "BatchId",
                principalTable: "Batchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
