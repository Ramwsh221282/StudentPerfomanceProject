using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.DataAccess.Module.Shared.Migrations
{
    /// <inheritdoc />
    public partial class StudentUniqueRecordBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Teachers_EntityNumber",
                table: "Teachers",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_EntityNumber",
                table: "Students",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Recordbook",
                table: "Students",
                column: "Recordbook",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_EntityNumber",
                table: "Semesters",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_EntityNumber",
                table: "SemesterPlans",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EntityNumber",
                table: "Groups",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                table: "Groups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EntityNumber",
                table: "Grades",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_EntityNumber",
                table: "EducationPlans",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_EntityNumber",
                table: "Disciplines",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EntityNumber",
                table: "Departments",
                column: "EntityNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_EntityNumber",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_EntityNumber",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Recordbook",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_EntityNumber",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_SemesterPlans_EntityNumber",
                table: "SemesterPlans");

            migrationBuilder.DropIndex(
                name: "IX_Groups_EntityNumber",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Name",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Grades_EntityNumber",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_EducationPlans_EntityNumber",
                table: "EducationPlans");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_EntityNumber",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EntityNumber",
                table: "Departments");
        }
    }
}
