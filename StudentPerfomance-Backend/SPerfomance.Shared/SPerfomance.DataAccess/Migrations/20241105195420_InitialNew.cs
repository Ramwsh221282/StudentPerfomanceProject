using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedToRecordBook_Recordbook",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignedTo_Name",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignedTo_Patronymic",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignedTo_Surname",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignerDepartment_Name",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Assigner_Name",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Assigner_Patronymic",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Assigner_Surname",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignetToGroup_Name",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignmentCloseDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssignmentOpenDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "State_State",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Value_Value",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Discipline_Name",
                table: "Assignments",
                newName: "DisciplineId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Weeks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StudentAssignment",
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
                    table.PrimaryKey("PK_StudentAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssignment_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssignment_Students_StudentId",
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
                name: "IX_StudentAssignment_AssignmentId",
                table: "StudentAssignment",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignment_StudentId",
                table: "StudentAssignment",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_SemesterPlans_DisciplineId",
                table: "Assignments",
                column: "DisciplineId",
                principalTable: "SemesterPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_SemesterPlans_DisciplineId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "StudentAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_DisciplineId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "DisciplineId",
                table: "Assignments",
                newName: "Discipline_Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Weeks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<ulong>(
                name: "AssignedToRecordBook_Recordbook",
                table: "Assignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo_Name",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo_Patronymic",
                table: "Assignments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo_Surname",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssignerDepartment_Name",
                table: "Assignments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assigner_Name",
                table: "Assignments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assigner_Patronymic",
                table: "Assignments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assigner_Surname",
                table: "Assignments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignetToGroup_Name",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignmentCloseDate",
                table: "Assignments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignmentOpenDate",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "State_State",
                table: "Assignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "Value_Value",
                table: "Assignments",
                type: "INTEGER",
                nullable: true);
        }
    }
}
