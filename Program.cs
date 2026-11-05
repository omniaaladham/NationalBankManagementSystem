using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Entities;
using OnlineLearningPlatform.Enums;

namespace OnlineLearningPlatform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new OnlineLearningContext();

            SeedStudentsAndEnrollments(context);

            Console.WriteLine("\n========== CREATE OPERATIONS ==========");

            int instructorId = AddInstructor(context);
            int courseId = AddCourse(context, instructorId, 3);

            List<int> studentIds = AddThreeStudents(context);

            EnrollStudent(context, studentIds[0], 1);

            Console.WriteLine("\n========== READ OPERATIONS ==========");

            FindCourseById(context, 1);
            FindCourseByTitle(context, "ASP.NET Core");
            GetCoursesByPriceRange(context, 400, 800);
            DisplayCategories(context, 1);
            DisplayCourseRoster(context, 1);

            Console.WriteLine("\n========== UPDATE OPERATIONS ==========");

            ChangeCoursePrice(context, 1, 550);
            UpdateEnrollmentProgress(context, 1, 1, 100);
            PublishCourse(context, 4);

            Console.WriteLine("\n========== DELETE OPERATIONS ==========");

            DeleteEnrollment(context, 4, 1);
            TryDeleteCategory(context, 1);
            SafeDeleteCategory(context, 1);
            DeleteCourseWithEnrollments(context, 5);

            Console.WriteLine("\n========== BULK OPERATIONS ==========");

            IncreasePublishedCoursePrices(context, CourseLevel.Beginner);
            DeleteDroppedEnrollments(context);
        }

        // PART 2: Seed students and enrollments
        static void SeedStudentsAndEnrollments(OnlineLearningContext context)
        {
            if (!context.Students.Any())
            {
                var students = new List<Student>
                {
                    new Student { FullName = "Ali Hassan", Email = "ali@example.com", DateOfBirth = new DateTime(2003, 5, 10) },
                    new Student { FullName = "Mona Ahmed", Email = "mona@example.com", DateOfBirth = new DateTime(2002, 8, 15) },
                    new Student { FullName = "Omar Khaled", Email = "omar@example.com", DateOfBirth = new DateTime(2003, 2, 20) },
                    new Student { FullName = "Sara Mohamed", Email = "sara@example.com", DateOfBirth = new DateTime(2002, 11, 5) },
                    new Student { FullName = "Youssef Ali", Email = "youssef@example.com", DateOfBirth = new DateTime(2003, 7, 25) }
                };

                context.Students.AddRange(students);
                context.SaveChanges();

                Console.WriteLine("5 students added successfully!");
            }

            if (!context.Enrollments.Any())
            {
                var enrollments = new List<Enrollment>
                {
                    new Enrollment { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now, Progress = 20, Status = EnrollmentStatus.Active },
                    new Enrollment { StudentId = 1, CourseId = 2, EnrollmentDate = DateTime.Now, Progress = 100, Status = EnrollmentStatus.Completed, FinalGrade = 95 },
                    new Enrollment { StudentId = 2, CourseId = 1, EnrollmentDate = DateTime.Now, Progress = 40, Status = EnrollmentStatus.Dropped },
                    new Enrollment { StudentId = 2, CourseId = 3, EnrollmentDate = DateTime.Now, Progress = 60, Status = EnrollmentStatus.Active },
                    new Enrollment { StudentId = 3, CourseId = 2, EnrollmentDate = DateTime.Now, Progress = 100, Status = EnrollmentStatus.Completed, FinalGrade = 88 },
                    new Enrollment { StudentId = 3, CourseId = 5, EnrollmentDate = DateTime.Now, Progress = 10, Status = EnrollmentStatus.Dropped },
                    new Enrollment { StudentId = 4, CourseId = 1, EnrollmentDate = DateTime.Now, Progress = 0, Status = EnrollmentStatus.Active },
                    new Enrollment { StudentId = 5, CourseId = 5, EnrollmentDate = DateTime.Now, Progress = 100, Status = EnrollmentStatus.Completed, FinalGrade = 92 }
                };

                context.Enrollments.AddRange(enrollments);
                context.SaveChanges();

                Console.WriteLine("8 enrollments added successfully!");
            }
        }

        // CREATE 1: Add instructor
        static int AddInstructor(OnlineLearningContext context)
        {
            string email = "new.instructor@example.com";

            var existingInstructor = context.Instructors
                .FirstOrDefault(i => i.Email == email);

            if (existingInstructor != null)
            {
                Console.WriteLine("Instructor already exists!");
                return existingInstructor.Id;
            }

            var instructor = new Instructor
            {
                FullName = "New Instructor",
                Email = email,
                Bio = "Full Stack Developer",
                JoinedAt = DateTime.Now
            };

            context.Instructors.Add(instructor);
            context.SaveChanges();

            Console.WriteLine("Instructor added successfully!");

            return instructor.Id;
        }

        // CREATE 2: Add course after checking instructor and category
        static int AddCourse(
            OnlineLearningContext context,
            int instructorId,
            int categoryId)
        {
            var existingCourse = context.Courses
                .FirstOrDefault(c => c.Title == "EF Core Advanced Demo");

            if (existingCourse != null)
            {
                Console.WriteLine("Course already exists!");
                return existingCourse.Id;
            }

            var instructor = context.Instructors.Find(instructorId);
            var category = context.Categories.Find(categoryId);

            if (instructor == null || category == null)
            {
                Console.WriteLine("Instructor or Category not found!");
                return 0;
            }

            var course = new Course
            {
                Title = "EF Core Advanced Demo",
                Description = "Learning advanced Entity Framework Core",
                Price = 800,
                Level = CourseLevel.Advanced,
                IsPublished = true,
                PublishedAt = DateTime.Now,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            context.Courses.Add(course);
            context.SaveChanges();

            Console.WriteLine("Course added successfully!");

            return course.Id;
        }

        // CREATE 3: Add three students using AddRange and one SaveChanges
        static List<int> AddThreeStudents(OnlineLearningContext context)
        {
            var studentData = new List<Student>
            {
                new Student { FullName = "Student One", Email = "student.one@example.com", DateOfBirth = new DateTime(2003, 1, 10) },
                new Student { FullName = "Student Two", Email = "student.two@example.com", DateOfBirth = new DateTime(2003, 2, 15) },
                new Student { FullName = "Student Three", Email = "student.three@example.com", DateOfBirth = new DateTime(2003, 3, 20) }
            };

            var newStudents = studentData
                .Where(s => !context.Students.Any(x => x.Email == s.Email))
                .ToList();

            if (newStudents.Any())
            {
                context.Students.AddRange(newStudents);
                context.SaveChanges();

                Console.WriteLine($"{newStudents.Count} students added successfully!");
            }
            else
            {
                Console.WriteLine("The three students already exist!");
            }

            return studentData
                .Select(s => context.Students.First(x => x.Email == s.Email).Id)
                .ToList();
        }

        // CREATE 4: Enroll student in a published course
        static void EnrollStudent(
            OnlineLearningContext context,
            int studentId,
            int courseId)
        {
            var student = context.Students.Find(studentId);
            var course = context.Courses.Find(courseId);

            if (student == null || course == null)
            {
                Console.WriteLine("Student or Course not found!");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine("Cannot enroll in an unpublished course!");
                return;
            }

            var existingEnrollment = context.Enrollments.Find(studentId, courseId);

            if (existingEnrollment != null)
            {
                Console.WriteLine("Student is already enrolled in this course!");
                return;
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                Progress = 0,
                Status = EnrollmentStatus.Active
            };

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            Console.WriteLine("Student enrolled successfully!");
        }

        // READ 1: Find course by ID using Find
        static void FindCourseById(
            OnlineLearningContext context,
            int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            Console.WriteLine(
                $"Course found: {course.Title}, Price: {course.Price}");
        }

        // READ 2: Find course by title using FirstOrDefault
        static void FindCourseByTitle(
            OnlineLearningContext context,
            string title)
        {
            var course = context.Courses
                .FirstOrDefault(c => c.Title == title);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            Console.WriteLine(
                $"Course found: {course.Title}, Level: {course.Level}");
        }

        // READ 3: Get courses within a price range
        static void GetCoursesByPriceRange(
            OnlineLearningContext context,
            decimal minPrice,
            decimal maxPrice)
        {
            var courses = context.Courses
                .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .ToList();

            Console.WriteLine(
                $"\nCourses between {minPrice} and {maxPrice}:");

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Title} - {course.Price}");
            }

            if (!courses.Any())
            {
                Console.WriteLine("No courses found in this price range.");
            }
        }

        // READ 4: Display subcategories and top-level categories
        static void DisplayCategories(
            OnlineLearningContext context,
            int parentId)
        {
            var subcategories = context.Categories
                .Where(c => c.ParentCategoryId == parentId)
                .ToList();

            Console.WriteLine("\nSubcategories:");

            foreach (var category in subcategories)
            {
                Console.WriteLine(category.Name);
            }

            var topLevelCategories = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine("\nTop-level categories:");

            foreach (var category in topLevelCategories)
            {
                Console.WriteLine(category.Name);
            }
        }

        // READ 5: Display course roster without Include or Join
        static void DisplayCourseRoster(
            OnlineLearningContext context,
            int courseId)
        {
            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            var students = context.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            Console.WriteLine(
                $"\nStudents enrolled in course {courseId}:");

            foreach (var student in students)
            {
                Console.WriteLine(student.FullName);
            }

            if (!students.Any())
            {
                Console.WriteLine("No students enrolled in this course.");
            }
        }

        // UPDATE 1: Change course price
        static void ChangeCoursePrice(
            OnlineLearningContext context,
            int courseId,
            decimal newPrice)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            course.Price = newPrice;
            context.SaveChanges();

            Console.WriteLine(
                $"Course price updated successfully! New price: {newPrice}");
        }

        // UPDATE 2: Update enrollment progress using composite key
        static void UpdateEnrollmentProgress(
            OnlineLearningContext context,
            int studentId,
            int courseId,
            decimal newProgress)
        {
            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine(
                    "Invalid progress! Progress must be between 0 and 100.");
                return;
            }

            var enrollment = context.Enrollments.Find(studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found!");
                return;
            }

            enrollment.Progress = newProgress;

            if (newProgress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
                enrollment.FinalGrade = 95;
            }
            else
            {
                enrollment.Status = EnrollmentStatus.Active;
                enrollment.FinalGrade = null;
            }

            context.SaveChanges();

            Console.WriteLine(
                $"Enrollment updated successfully! Progress: {enrollment.Progress}%, Status: {enrollment.Status}");
        }

        // UPDATE 3: Publish course and set publication date
        static void PublishCourse(
            OnlineLearningContext context,
            int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            if (course.IsPublished)
            {
                Console.WriteLine("Course is already published!");
                return;
            }

            course.IsPublished = true;
            course.PublishedAt = DateTime.Now;

            context.SaveChanges();

            Console.WriteLine("Course published successfully!");
        }

        // DELETE 1: Delete enrollment using composite key
        static void DeleteEnrollment(
            OnlineLearningContext context,
            int studentId,
            int courseId)
        {
            context.ChangeTracker.Clear();

            var enrollment = context.Enrollments.Find(studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found!");
                return;
            }

            context.Enrollments.Remove(enrollment);
            context.SaveChanges();

            Console.WriteLine("Enrollment deleted successfully!");
        }

        // DELETE 2: Try deleting a category with subcategories
        static void TryDeleteCategory(
            OnlineLearningContext context,
            int categoryId)
        {
            context.ChangeTracker.Clear();

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found!");
                return;
            }

            context.Categories.Remove(category);

            try
            {
                context.SaveChanges();
                Console.WriteLine("Category deleted successfully!");
            }
            catch (DbUpdateException)
            {
                Console.WriteLine(
                    "Cannot delete category because it has subcategories or related courses!");

                context.Entry(category).State = EntityState.Unchanged;
            }
        }

        // DELETE 3: Safely delete a category
        static void SafeDeleteCategory(
            OnlineLearningContext context,
            int categoryId)
        {
            context.ChangeTracker.Clear();

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found!");
                return;
            }

            bool hasSubcategories = context.Categories
                .Any(c => c.ParentCategoryId == categoryId);

            bool hasCourses = context.Courses
                .Any(c => c.CategoryId == categoryId);

            if (hasSubcategories || hasCourses)
            {
                Console.WriteLine(
                    "Category cannot be deleted because it has related data!");

                return;
            }

            context.Categories.Remove(category);
            context.SaveChanges();

            Console.WriteLine("Category deleted successfully!");
        }

        // DELETE 4: Delete course and its enrollments
        static void DeleteCourseWithEnrollments(
            OnlineLearningContext context,
            int courseId)
        {
            context.ChangeTracker.Clear();

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            int enrollmentsBefore = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                $"Enrollments before deleting course: {enrollmentsBefore}");

            // Cascade delete automatically removes related enrollments
            context.Courses.Remove(course);
            context.SaveChanges();

            int enrollmentsAfter = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine("Course deleted successfully!");

            Console.WriteLine(
                $"Enrollments after deleting course: {enrollmentsAfter}");
        }

        // BULK 1: Increase published course prices by 10%
        static void IncreasePublishedCoursePrices(
            OnlineLearningContext context,
            CourseLevel level)
        {
            context.ChangeTracker.Clear();

            int affectedRows = context.Courses
                .Where(c => c.IsPublished && c.Level == level)
                .ExecuteUpdate(setters => setters
                    .SetProperty(c => c.Price, c => c.Price * 1.10m));

            Console.WriteLine(
                $"Published course prices increased by 10%. Affected rows: {affectedRows}");
        }

        // BULK 2: Delete all dropped enrollments
        static void DeleteDroppedEnrollments(
            OnlineLearningContext context)
        {
            context.ChangeTracker.Clear();

            int affectedRows = context.Enrollments
                .Where(e => e.Status == EnrollmentStatus.Dropped)
                .ExecuteDelete();

            Console.WriteLine(
                $"Dropped enrollments deleted successfully! Affected rows: {affectedRows}");
        }
    }
}