using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.DataAccess.Module.Shared.Migrations
{
    /// <inheritdoc />
    public partial class DirectionCodeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EducationDirections_DirectionCode",
                table: "EducationDirections",
                column: "DirectionCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EducationDirections_DirectionCode",
                table: "EducationDirections");
        }
    }
}
