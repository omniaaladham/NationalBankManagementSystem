using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;

namespace OnlineLearningPlatform.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Table name
            builder.ToTable("Students");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Full Name
            builder.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(150);

            // Email
            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Date Of Birth
            builder.Property(s => s.DateOfBirth)
                .IsRequired();

            // Unique Email
            builder.HasIndex(s => s.Email)
                .IsUnique();
        }
    }
}