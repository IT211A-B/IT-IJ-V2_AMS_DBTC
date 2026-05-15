using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMS_Backend_V2.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseCodeToCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Courses",
                newName: "CourseCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "Courses",
                newName: "CourseName");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Attendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
