using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.Statistics.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlWeekReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RowNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsFinished = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlWeekReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseReports_ControlWeekReports_RootId",
                        column: x => x.RootId,
                        principalTable: "ControlWeekReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentStatisticsReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentStatisticsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentStatisticsReports_ControlWeekReports_RootId",
                        column: x => x.RootId,
                        principalTable: "ControlWeekReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionCodeReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionCodeReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionCodeReports_ControlWeekReports_RootId",
                        column: x => x.RootId,
                        principalTable: "ControlWeekReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionTypeReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionTypeReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionTypeReports_ControlWeekReports_RootId",
                        column: x => x.RootId,
                        principalTable: "ControlWeekReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupStatisticsReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: false),
                    AtSemester = table.Column<byte>(type: "INTEGER", nullable: false),
                    DirectionCode = table.Column<string>(type: "TEXT", nullable: false),
                    DirectionType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStatisticsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupStatisticsReports_ControlWeekReports_RootId",
                        column: x => x.RootId,
                        principalTable: "ControlWeekReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseReportEntityParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false),
                    Course = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReportEntityParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseReportEntityParts_CourseReports_RootId",
                        column: x => x.RootId,
                        principalTable: "CourseReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentStatisticsReportParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartmentName = table.Column<string>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentStatisticsReportParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentStatisticsReportParts_DepartmentStatisticsReports_RootId",
                        column: x => x.RootId,
                        principalTable: "DepartmentStatisticsReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionCodeReportParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionCodeReportParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionCodeReportParts_DirectionCodeReports_RootId",
                        column: x => x.RootId,
                        principalTable: "DirectionCodeReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionTypeReportParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DirectionType = table.Column<string>(type: "TEXT", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionTypeReportParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionTypeReportParts_DirectionTypeReports_RootId",
                        column: x => x.RootId,
                        principalTable: "DirectionTypeReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentStatisticsReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentName = table.Column<string>(type: "TEXT", nullable: false),
                    StudentSurname = table.Column<string>(type: "TEXT", nullable: false),
                    StudentPatronymic = table.Column<string>(type: "TEXT", nullable: true),
                    StudentRecordBook = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStatisticsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentStatisticsReports_GroupStatisticsReports_RootId",
                        column: x => x.RootId,
                        principalTable: "GroupStatisticsReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherStatisticsReportParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeacherName = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherSurname = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherPatronymic = table.Column<string>(type: "TEXT", nullable: true),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStatisticsReportParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherStatisticsReportParts_DepartmentStatisticsReportParts_RootId",
                        column: x => x.RootId,
                        principalTable: "DepartmentStatisticsReportParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentStatisticsOnDisciplines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DisciplineName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStatisticsOnDisciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentStatisticsOnDisciplines_StudentStatisticsReports_RootId",
                        column: x => x.RootId,
                        principalTable: "StudentStatisticsReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseReportEntityParts_RootId",
                table: "CourseReportEntityParts",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReports_RootId",
                table: "CourseReports",
                column: "RootId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentStatisticsReportParts_RootId",
                table: "DepartmentStatisticsReportParts",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentStatisticsReports_RootId",
                table: "DepartmentStatisticsReports",
                column: "RootId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectionCodeReportParts_RootId",
                table: "DirectionCodeReportParts",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionCodeReports_RootId",
                table: "DirectionCodeReports",
                column: "RootId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectionTypeReportParts_RootId",
                table: "DirectionTypeReportParts",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionTypeReports_RootId",
                table: "DirectionTypeReports",
                column: "RootId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupStatisticsReports_RootId",
                table: "GroupStatisticsReports",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStatisticsOnDisciplines_RootId",
                table: "StudentStatisticsOnDisciplines",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStatisticsReports_RootId",
                table: "StudentStatisticsReports",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStatisticsReportParts_RootId",
                table: "TeacherStatisticsReportParts",
                column: "RootId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseReportEntityParts");

            migrationBuilder.DropTable(
                name: "DirectionCodeReportParts");

            migrationBuilder.DropTable(
                name: "DirectionTypeReportParts");

            migrationBuilder.DropTable(
                name: "StudentStatisticsOnDisciplines");

            migrationBuilder.DropTable(
                name: "TeacherStatisticsReportParts");

            migrationBuilder.DropTable(
                name: "CourseReports");

            migrationBuilder.DropTable(
                name: "DirectionCodeReports");

            migrationBuilder.DropTable(
                name: "DirectionTypeReports");

            migrationBuilder.DropTable(
                name: "StudentStatisticsReports");

            migrationBuilder.DropTable(
                name: "DepartmentStatisticsReportParts");

            migrationBuilder.DropTable(
                name: "GroupStatisticsReports");

            migrationBuilder.DropTable(
                name: "DepartmentStatisticsReports");

            migrationBuilder.DropTable(
                name: "ControlWeekReports");
        }
    }
}
