using Curriculum.Models;

namespace Curriculum.Service
{
    public interface ICourseService
    {
        List<Courses> GetCourseList();
        void PushCourse(Courses course);
        void DeleteCourse(string CourseCode);
        void UpdateCourse(Courses course);
        List<Courses> SearchCourses(string search);

    }
}
