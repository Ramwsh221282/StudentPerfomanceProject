﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPerfomance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TeacherWithUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Teachers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Teachers");
        }
    }
}
