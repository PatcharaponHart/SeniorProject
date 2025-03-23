using Curriculum.Models;
using Curriculum.Repositorys;

namespace Curriculum.Service
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public List<Grades> GetGradeList()
        {
            var result = _gradeRepository.GetGradeList();
            return result;
        }
        public void PushGrade(Grades grade)
        {
            _gradeRepository.PushGrade(grade);
        }

        //public void DeleteGrade(string CourseCode)
        //{
        //    _gradeRepository.DeleteGrade(CourseCode);
        //}
        public void UpdateGrade(Grades grade)
        {
            _gradeRepository.UpdateGrade(grade);
        }
        public List<Grades> SearchGrades(string search)
        {
            var result = _gradeRepository.SearchGrades(search);
            return result;
        }
    }
}
