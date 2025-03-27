using Curriculum.Models;

namespace Curriculum.Service
{
    public interface IGradeService
    {
        Task<IEnumerable<StudentGradeCourseDto>> GetStudentGradesWithCourseDetailsAsync();
        List<Grades> GetGradeList();
        void PushGrade(Grades grade);
        void PushGrades(List<Grades> grades);
        void UpdateGrade(Grades grade);
        void UpdateGrades(List<Grades> gradesList);
        void UpdateGradeByStudent(Grades grade);
        List<Grades> SearchGrades(string search);
    }
}
