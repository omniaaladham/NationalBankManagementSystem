using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;

namespace OnlineLearningPlatform.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            // Table name
            builder.ToTable("Instructors");

            // Primary Key
            builder.HasKey(i => i.Id);

            // Full Name
            builder.Property(i => i.FullName)
                .IsRequired()
                .HasMaxLength(150);

            // Email
            builder.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Bio
            builder.Property(i => i.Bio)
                .IsRequired(false)
                .HasMaxLength(500);

            // Joined Date
            builder.Property(i => i.JoinedAt)
                .IsRequired();




            // Seed Data
            builder.HasData(
                new Instructor
                {
                    Id = 1,
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed@example.com",
                    Bio = "Backend Developer",
                    JoinedAt = new DateTime(2025, 1, 10)
                },
                new Instructor
                {
                    Id = 2,
                    FullName = "Sara Ali",
                    Email = "sara@example.com",
                    Bio = "Frontend Developer",
                    JoinedAt = new DateTime(2025, 2, 15)
                },
                new Instructor
                {
                    Id = 3,
                    FullName = "Omar Hassan",
                    Email = "omar@example.com",
                    Bio = "Database Specialist",
                    JoinedAt = new DateTime(2025, 3, 20)
                }
            );
        }
    }
}