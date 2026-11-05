using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;

namespace OnlineLearningPlatform.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            // Table name
            builder.ToTable("Enrollments", table =>
            {
                table.HasCheckConstraint(
                    "CK_Enrollments_Progress",
                    "[Progress] >= 0 AND [Progress] <= 100");
            });

            // Composite Primary Key
            builder.HasKey(e => new { e.StudentId, e.CourseId });

            // Enrollment Date
            builder.Property(e => e.EnrollmentDate)
                .IsRequired();

            // Progress
            builder.Property(e => e.Progress)
                .HasPrecision(5, 2)
                .IsRequired();

            // Status
            builder.Property(e => e.Status)
                .IsRequired();

            // Final Grade
            builder.Property(e => e.FinalGrade)
                .HasPrecision(5, 2)
                .IsRequired(false);

            // Student Relationship
            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course Relationship
            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}