using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StudentGroupsAddedAttributeGroupEducationDirection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupEducationDirection",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupEducationDirection",
                table: "Groups");
        }
    }
}
