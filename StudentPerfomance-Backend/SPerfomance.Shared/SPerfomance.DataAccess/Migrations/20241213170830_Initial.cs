using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Acronymus = table.Column<string>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDirections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type_Type = table.Column<string>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDirections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SessionStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SessionCloseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Number_Number = table.Column<byte>(type: "INTEGER", nullable: false),
                    State_State = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type_Type = table.Column<string>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Name_Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Name_Patronymic = table.Column<string>(type: "TEXT", nullable: true),
                    Email_Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role_Role = table.Column<string>(type: "TEXT", nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AttachedRoleId = table.Column<string>(type: "TEXT", nullable: true),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JobTitle_JobTitle = table.Column<string>(type: "TEXT", nullable: false),
                    State_State = table.Column<string>(type: "TEXT", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Name_Patronymic = table.Column<string>(type: "TEXT", nullable: true),
                    Name_Surname = table.Column<string>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DirectionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year_Year = table.Column<int>(type: "INTEGER", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlanId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number_Number = table.Column<byte>(type: "INTEGER", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_EducationPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "EducationPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name_Name = table.Column<string>(type: "TEXT", nullable: false),
                    EducationPlanId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ActiveGroupSemesterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_EducationPlans_EducationPlanId",
                        column: x => x.EducationPlanId,
                        principalTable: "EducationPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Groups_Semesters_ActiveGroupSemesterId",
                        column: x => x.ActiveGroupSemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SemesterPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discipline_Name = table.Column<string>(type: "TEXT", nullable: false),
                    SemesterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeacherId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterPlans_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterPlans_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Name_Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Name_Patronymic = table.Column<string>(type: "TEXT", nullable: true),
                    State_State = table.Column<string>(type: "TEXT", nullable: false),
                    Recordbook_Recordbook = table.Column<ulong>(type: "INTEGER", nullable: false),
                    AttachedGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_AttachedGroupId",
                        column: x => x.AttachedGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weeks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Weeks_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    WeekId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DisciplineId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_SemesterPlans_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "SemesterPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value_Value = table.Column<byte>(type: "INTEGER", nullable: false),
                    EntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssignments_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssignments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_DisciplineId",
                table: "Assignments",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_EntityNumber",
                table: "Assignments",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_WeekId",
                table: "Assignments",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EntityNumber",
                table: "Departments",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationDirections_Code_Code",
                table: "EducationDirections",
                column: "Code_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationDirections_EntityNumber",
                table: "EducationDirections",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_DirectionId",
                table: "EducationPlans",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_EntityNumber",
                table: "EducationPlans",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ActiveGroupSemesterId",
                table: "Groups",
                column: "ActiveGroupSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EntityNumber",
                table: "Groups",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name_Name",
                table: "Groups",
                column: "Name_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_EntityNumber",
                table: "SemesterPlans",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_SemesterId",
                table: "SemesterPlans",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlans_TeacherId",
                table: "SemesterPlans",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_EntityNumber",
                table: "Semesters",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_PlanId",
                table: "Semesters",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_EntityNumber",
                table: "Sessions",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignments_AssignmentId",
                table: "StudentAssignments",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignments_StudentId",
                table: "StudentAssignments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AttachedGroupId",
                table: "Students",
                column: "AttachedGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_EntityNumber",
                table: "Students",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Recordbook_Recordbook",
                table: "Students",
                column: "Recordbook_Recordbook",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DepartmentId",
                table: "Teachers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_EntityNumber",
                table: "Teachers",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EntityNumber",
                table: "Users",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_EntityNumber",
                table: "Weeks",
                column: "EntityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_GroupId",
                table: "Weeks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_SessionId",
                table: "Weeks",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAssignments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SemesterPlans");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "EducationPlans");

            migrationBuilder.DropTable(
                name: "EducationDirections");
        }
    }
}
