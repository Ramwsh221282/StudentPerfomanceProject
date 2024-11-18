using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.Statistics.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FirstStatistics : Migration
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
                    CompletionDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlWeekReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseStatisticsReportEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DirectionType = table.Column<string>(type: "TEXT", nullable: false),
                    Course = table.Column<int>(type: "INTEGER", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStatisticsReportEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseStatisticsReportEntities_ControlWeekReports_RootId",
                        column: x => x.RootId,
                        principalTable: "ControlWeekReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionCodeStatisticsReportEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DirectionCode = table.Column<string>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionCodeStatisticsReportEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionCodeStatisticsReportEntities_ControlWeekReports_RootId",
                        column: x => x.RootId,
                        principalTable: "ControlWeekReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionTypeStatisticsReportEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DirectionType = table.Column<string>(type: "TEXT", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionTypeStatisticsReportEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionTypeStatisticsReportEntities_ControlWeekReports_RootId",
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
                    DirectionCode = table.Column<string>(type: "TEXT", nullable: false),
                    DirectionType = table.Column<string>(type: "TEXT", nullable: false),
                    Course = table.Column<byte>(type: "INTEGER", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "DisciplinesStatisticsReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DisciplineName = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherName = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherSurname = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherPatronymic = table.Column<string>(type: "TEXT", nullable: true),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinesStatisticsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinesStatisticsReports_GroupStatisticsReports_RootId",
                        column: x => x.RootId,
                        principalTable: "GroupStatisticsReports",
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
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Perfomance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStatisticsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentStatisticsReports_DisciplinesStatisticsReports_RootId",
                        column: x => x.RootId,
                        principalTable: "DisciplinesStatisticsReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStatisticsReportEntities_RootId",
                table: "CourseStatisticsReportEntities",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionCodeStatisticsReportEntities_RootId",
                table: "DirectionCodeStatisticsReportEntities",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionTypeStatisticsReportEntities_RootId",
                table: "DirectionTypeStatisticsReportEntities",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinesStatisticsReports_RootId",
                table: "DisciplinesStatisticsReports",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStatisticsReports_RootId",
                table: "GroupStatisticsReports",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStatisticsReports_RootId",
                table: "StudentStatisticsReports",
                column: "RootId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStatisticsReportEntities");

            migrationBuilder.DropTable(
                name: "DirectionCodeStatisticsReportEntities");

            migrationBuilder.DropTable(
                name: "DirectionTypeStatisticsReportEntities");

            migrationBuilder.DropTable(
                name: "StudentStatisticsReports");

            migrationBuilder.DropTable(
                name: "DisciplinesStatisticsReports");

            migrationBuilder.DropTable(
                name: "GroupStatisticsReports");

            migrationBuilder.DropTable(
                name: "ControlWeekReports");
        }
    }
}
