using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.DataAccess.Module.Shared.Migrations
{
    /// <inheritdoc />
    public partial class ModelsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemesterPlans_Disciplines_LinkedDisciplineId",
                table: "SemesterPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterPlans_Semesters_LinkedSemesterId",
                table: "SemesterPlans");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_SemesterPlans_LinkedDisciplineId",
                table: "SemesterPlans");

            migrationBuilder.DropIndex(
                name: "IX_SemesterPlans_LinkedSemesterId",
                table: "SemesterPlans");

            migrationBuilder.DropColumn(
                name: "LinkedDisciplineId",
                table: "SemesterPlans");

            migrationBuilder.RenameColumn(
                name: "PlanName",
                table: "SemesterPlans",
                newName: "SemesterId");

            migrationBuilder.RenameColumn(
                name: "LinkedSemesterId",
                table: "SemesterPlans",
                newName: "Discipline_Name");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachedTeacherId",
                table: "SemesterPlans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_AttachedTeacherId",
                table: "SemesterPlans",
                column: "AttachedTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_SemesterId",
                table: "SemesterPlans",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterPlans_Semesters_SemesterId",
                table: "SemesterPlans",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterPlans_Teachers_AttachedTeacherId",
                table: "SemesterPlans",
                column: "AttachedTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemesterPlans_Semesters_SemesterId",
                table: "SemesterPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterPlans_Teachers_AttachedTeacherId",
                table: "SemesterPlans");

            migrationBuilder.DropIndex(
                name: "IX_SemesterPlans_AttachedTeacherId",
                table: "SemesterPlans");

            migrationBuilder.DropIndex(
                name: "IX_SemesterPlans_SemesterId",
                table: "SemesterPlans");

            migrationBuilder.DropColumn(
                name: "AttachedTeacherId",
                table: "SemesterPlans");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "SemesterPlans",
                newName: "PlanName");

            migrationBuilder.RenameColumn(
                name: "Discipline_Name",
                table: "SemesterPlans",
                newName: "LinkedSemesterId");

            migrationBuilder.AddColumn<Guid>(
                name: "LinkedDisciplineId",
                table: "SemesterPlans",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeacherId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DisciplineId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeacherId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    GradeDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value_Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_LinkedDisciplineId",
                table: "SemesterPlans",
                column: "LinkedDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_LinkedSemesterId",
                table: "SemesterPlans",
                column: "LinkedSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_EntityNumber",
                table: "Disciplines",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Name",
                table: "Disciplines",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TeacherId",
                table: "Disciplines",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_DisciplineId",
                table: "Grades",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EntityNumber",
                table: "Grades",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TeacherId",
                table: "Grades",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterPlans_Disciplines_LinkedDisciplineId",
                table: "SemesterPlans",
                column: "LinkedDisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterPlans_Semesters_LinkedSemesterId",
                table: "SemesterPlans",
                column: "LinkedSemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
