using Curriculum.Models;

namespace Curriculum.Repositorys
{
    public interface IGradeRepository
    {
        List<Grades> GetGradeList();
        void PushGrade(Grades grade);
        void UpdateGrade(Grades grade);
        List<Grades> SearchGrades(string search);

    }
}
