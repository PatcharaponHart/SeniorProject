using Curriculum.Models;
using Curriculum.Repositorys;

namespace Curriculum.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository agentRepository)
        {
            _courseRepository = agentRepository;
        }

        public List<Courses> GetCourseList()
        {
            var result = _courseRepository.GetCourseList();
            return result;
        }
        public void PushCourse(Courses course)
        {
            _courseRepository.PushCourse(course);
        }

        public void DeleteCourse(string CourseCode)
        {
            _courseRepository.DeleteCourse(CourseCode);
        }
        public void UpdateCourse(Courses course)
        {
            _courseRepository.UpdateCourse(course);
        }
        public List<Courses> SearchCourses(string search)
        {
            var result = _courseRepository.SearchCourses(search);
            return result;
        }
    }
}
