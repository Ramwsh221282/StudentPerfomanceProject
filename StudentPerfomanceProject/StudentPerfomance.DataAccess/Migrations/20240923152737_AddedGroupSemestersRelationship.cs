using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedGroupSemestersRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Groups_GroupId",
                table: "Semesters");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Semesters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

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
