using Curriculum.Models;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace YourNamespace.Data // ตรวจสอบให้ตรงกับ namespace ของคุณ
{
    public class CurriculumDbContext : DbContext
    {
        // Constructor สำหรับ DbContext
        public CurriculumDbContext(DbContextOptions<CurriculumDbContext> options)
            : base(options)
        {
        }

        // ตัวอย่าง DbSet
        public DbSet<Students> tblStudents { get; set; }
        public DbSet<Courses> tblCourses { get; set; }
        public DbSet<Grades> tblGrades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grades>()
                .HasKey(g => new { g.StudentID, g.CourseCode }); // 🔥 Composite Primary Key
        }
    }
}
