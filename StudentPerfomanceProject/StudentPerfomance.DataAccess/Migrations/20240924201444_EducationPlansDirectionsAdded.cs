using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EducationPlansDirectionsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters");

            migrationBuilder.RenameColumn(
                name: "GroupEducationDirection",
                table: "Groups",
                newName: "EducationPlanId");

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "Teachers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Semesters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "Semesters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Semesters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "SemesterPlans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "Groups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "Grades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "Disciplines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityNumber",
                table: "Departments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EducationDirections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DirectionCode = table.Column<string>(type: "TEXT", nullable: false),
                    DirectionName = table.Column<string>(type: "TEXT", nullable: false),
                    DirectionType = table.Column<string>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDirections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DirectionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    YearOfCreation = table.Column<uint>(type: "INTEGER", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationPlans_EducationDirections_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "EducationDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_PlanId",
                table: "Semesters",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_DirectionId",
                table: "EducationPlans",
                column: "DirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_EducationPlans_PlanId",
                table: "Semesters",
                column: "PlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_EducationPlans_PlanId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters");

            migrationBuilder.DropTable(
                name: "EducationPlans");

            migrationBuilder.DropTable(
                name: "EducationDirections");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_PlanId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Groups_EducationPlanId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "SemesterPlans");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "EntityNumber",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "EducationPlanId",
                table: "Groups",
                newName: "GroupEducationDirection");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Semesters",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
