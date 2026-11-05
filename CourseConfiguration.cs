using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;
using OnlineLearningPlatform.Enums;

namespace OnlineLearningPlatform.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // Table name
            builder.ToTable("Courses");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Title
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Description
            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            // Price
            builder.Property(c => c.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            // Level
            builder.Property(c => c.Level)
                .IsRequired();

            // Published Status
            builder.Property(c => c.IsPublished)
                .IsRequired();

            // Published Date
            builder.Property(c => c.PublishedAt)
                .IsRequired(false);

            // Instructor Relationship
            builder.HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category Relationship
            builder.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Data
            builder.HasData(
                new Course
                {
                    Id = 1,
                    Title = "C# Fundamentals",
                    Description = "Learn C# basics",
                    Price = 500,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 1),
                    InstructorId = 1,
                    CategoryId = 2
                },
                new Course
                {
                    Id = 2,
                    Title = "ASP.NET Core",
                    Description = "Build web applications",
                    Price = 900,
                    Level = CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 5),
                    InstructorId = 1,
                    CategoryId = 3
                },
                new Course
                {
                    Id = 3,
                    Title = "HTML and CSS",
                    Description = "Frontend fundamentals",
                    Price = 400,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 10),
                    InstructorId = 2,
                    CategoryId = 4
                },
                new Course
                {
                    Id = 4,
                    Title = "Advanced JavaScript",
                    Description = "Advanced JavaScript concepts",
                    Price = 750,
                    Level = CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = 2,
                    CategoryId = 4
                },
                new Course
                {
                    Id = 5,
                    Title = "SQL Server",
                    Description = "Learn database management",
                    Price = 600,
                    Level = CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 15),
                    InstructorId = 3,
                    CategoryId = 5
                },
                new Course
                {
                    Id = 6,
                    Title = "Advanced Database Design",
                    Description = "Advanced database concepts",
                    Price = 1000,
                    Level = CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = 3,
                    CategoryId = 5
                }
            );
        }
    }
}