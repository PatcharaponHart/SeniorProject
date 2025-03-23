using Curriculum.Models;
using YourNamespace.Data;

namespace Curriculum.Repositorys
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CurriculumDbContext _context;

        public CourseRepository(CurriculumDbContext context)
        {
            _context = context;
        }

        public List<Courses> GetCourseList()
        {

            var result = _context.tblCourses.ToList();

            return result;
        }

        public void PushCourse(Courses course)
        {
            _context.tblCourses.Add(course);
            _context.SaveChanges();
        }

        public void DeleteCourse(string courseCode)
        {
            var courseToDelete = _context.tblCourses.Find(courseCode);
            if (courseToDelete != null)
            {
                _context.tblCourses.Remove(courseToDelete);
                _context.SaveChanges();
            }
        }
        public void UpdateCourse(Courses course)
        {
            var updateCourse = _context.tblCourses.Find(course.CourseCode);
            updateCourse.CourseNameTH = course.CourseNameTH;
            updateCourse.CourseNameEN = course.CourseNameEN;
            updateCourse.Credit = course.Credit;
            updateCourse.SubjectGroup = course.SubjectGroup;

            _context.SaveChanges();
        }
        public List<Courses> SearchCourses(string search)
        {

            var result = _context.tblCourses.Where(a =>
                a.CourseCode.Contains(search) ||
                a.CourseNameTH.Contains(search) ||
                a.CourseNameEN.Contains(search) ||
                string.IsNullOrEmpty(search)).ToList();


            return result;
        }
    }
}
