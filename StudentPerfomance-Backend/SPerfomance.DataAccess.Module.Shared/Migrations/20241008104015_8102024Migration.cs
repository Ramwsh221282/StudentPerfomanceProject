using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.DataAccess.Module.Shared.Migrations
{
    /// <inheritdoc />
    public partial class _8102024Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups");

            migrationBuilder.AlterColumn<Guid>(
                name: "EducationPlanId",
                table: "Groups",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups");

            migrationBuilder.AlterColumn<Guid>(
                name: "EducationPlanId",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationPlans_EducationPlanId",
                table: "Groups",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
