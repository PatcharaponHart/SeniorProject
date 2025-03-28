using Curriculum.Models;

namespace Curriculum.Repositorys
{
    public interface IGradeRepository
    {
        Task<StudentGradesSummaryDto> GetStudentGradesByStudentId(string studentId);
        Task<IEnumerable<StudentGradeCourseDto>> GetStudentGradesWithCourseDetailsAsync();
        List<Grades> GetGradeList();
        void PushGrade(Grades grade);
        void PushMultipleGrades(List<Grades> grades);
        void UpdateGrade(Grades grade);
        List<Grades> SearchGrades(string search);
        void UpdateGradeByStudent(Grades grade);
        void UpdateGrades(List<Grades> gradesList);

    }
}
