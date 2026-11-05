using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;

namespace OnlineLearningPlatform.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table name
            builder.ToTable("Categories");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Name
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Description
            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            // Self-Referencing Relationship
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Data
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Programming",
                    Description = "Programming courses",
                    ParentCategoryId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Backend",
                    Description = "Backend development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "ASP.NET",
                    Description = "ASP.NET development",
                    ParentCategoryId = 2
                },
                new Category
                {
                    Id = 4,
                    Name = "Frontend",
                    Description = "Frontend development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 5,
                    Name = "Databases",
                    Description = "Database courses",
                    ParentCategoryId = 1
                }
            );
        }
    }
}