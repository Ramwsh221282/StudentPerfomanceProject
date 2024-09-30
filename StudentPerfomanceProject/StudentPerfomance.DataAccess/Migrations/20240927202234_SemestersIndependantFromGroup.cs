using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SemestersIndependantFromGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_GroupId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Semesters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Semesters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_GroupId",
                table: "Semesters",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
