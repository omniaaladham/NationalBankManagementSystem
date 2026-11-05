using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 1, "Programming courses", "Programming", null });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Bio", "Email", "FullName", "JoinedAt" },
                values: new object[,]
                {
                    { 1, "Backend Developer", "ahmed@example.com", "Ahmed Mohamed", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Frontend Developer", "sara@example.com", "Sara Ali", new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Database Specialist", "omar@example.com", "Omar Hassan", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 2, "Backend development", "Backend", 1 },
                    { 4, "Frontend development", "Frontend", 1 },
                    { 5, "Database courses", "Databases", 1 },
                    { 3, "ASP.NET development", "ASP.NET", 2 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedAt", "Title" },
                values: new object[,]
                {
                    { 1, 2, "Learn C# basics", 1, true, 0, 500m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Fundamentals" },
                    { 3, 4, "Frontend fundamentals", 2, true, 0, 400m, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "HTML and CSS" },
                    { 4, 4, "Advanced JavaScript concepts", 2, false, 2, 750m, null, "Advanced JavaScript" },
                    { 5, 5, "Learn database management", 3, true, 1, 600m, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "SQL Server" },
                    { 6, 5, "Advanced database concepts", 3, false, 2, 1000m, null, "Advanced Database Design" },
                    { 2, 3, "Build web applications", 1, true, 1, 900m, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
