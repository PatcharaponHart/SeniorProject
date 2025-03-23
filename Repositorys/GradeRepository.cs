using Curriculum.Models;
using YourNamespace.Data;

namespace Curriculum.Repositorys
{
    public class GradeRepository : IGradeRepository
    {
        private readonly CurriculumDbContext _context;
        public GradeRepository(CurriculumDbContext context)
        {
            _context = context;
        }
        public List<Grades> GetGradeList()
        {

            var result = _context.tblGrades.ToList();

            return result;
        }

        public void PushGrade(Grades grade)
        {
            _context.tblGrades.Add(grade);
            _context.SaveChanges();
        }

        //public void DeleteGrade(string courseCode)
        //{
        //    var courseToDelete = _context.tblGrades.Find(courseCode);
        //    if (courseToDelete != null)
        //    {
        //        _context.tblCourses.Remove(courseToDelete);
        //        _context.SaveChanges();
        //    }
        //}
        public void UpdateGrade(Grades grade)
        {
            var updateGrade = _context.tblGrades.Find(grade.StudentID);
            updateGrade.CourseCode = grade.CourseCode;
            updateGrade.Grade = grade.Grade;
            updateGrade.Academic_year = grade.Academic_year;
            updateGrade.Semester = grade.Semester;
            updateGrade.Credit = grade.Credit;

            _context.SaveChanges();
        }
        public List<Grades> SearchGrades(string search)
        {

            var result = _context.tblGrades.Where(a =>
                a.StudentID.Contains(search) ||
                string.IsNullOrEmpty(search)).ToList();


            return result;
        }
    }
}
