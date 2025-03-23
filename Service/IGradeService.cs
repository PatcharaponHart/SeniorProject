using Curriculum.Models;

namespace Curriculum.Service
{
    public interface IGradeService
    {
        List<Grades> GetGradeList();
        void PushGrade(Grades grade);
        void UpdateGrade(Grades grade);
        List<Grades> SearchGrades(string search);
    }
}
