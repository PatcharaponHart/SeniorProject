using Curriculum.Models;
using Curriculum.Repositorys;
using System.Collections.Generic;

namespace Curriculum.Service
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }
        public async Task<StudentGradesSummaryDto> GetStudentGradesByStudentId(string studentId)
        {
            return await _gradeRepository.GetStudentGradesByStudentId(studentId);
        }
        public async Task<IEnumerable<StudentGradeCourseDto>> GetStudentGradesWithCourseDetailsAsync()
        {
            return await _gradeRepository.GetStudentGradesWithCourseDetailsAsync();
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
        public void PushGrades(List<Grades> grades)
        {
            _gradeRepository.PushMultipleGrades(grades);
        }

        //public void DeleteGrade(string CourseCode)
        //{
        //    _gradeRepository.DeleteGrade(CourseCode);
        //}
        public void UpdateGrade(Grades grade)
        {
            _gradeRepository.UpdateGrade(grade);
        }
        public void UpdateGradeByStudent(Grades grade)
        {
            _gradeRepository.UpdateGradeByStudent(grade);
        }
        public void UpdateGrades(List<Grades> gradesList)
        {
            _gradeRepository.UpdateGrades(gradesList);
        }
        public List<Grades> SearchGrades(string search)
        {
            var result = _gradeRepository.SearchGrades(search);
            return result;
        }
    }
}
