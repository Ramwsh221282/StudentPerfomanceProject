using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SemestersImplemented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number_Value = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlanName = table.Column<string>(type: "TEXT", nullable: false),
                    LinkedSemesterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LinkedDisciplineId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterPlans_Disciplines_LinkedDisciplineId",
                        column: x => x.LinkedDisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterPlans_Semesters_LinkedSemesterId",
                        column: x => x.LinkedSemesterId,
                        principalTable: "Semesters",
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
                name: "IX_Semesters_GroupId",
                table: "Semesters",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterPlans");

            migrationBuilder.DropTable(
                name: "Semesters");
        }
    }
}
