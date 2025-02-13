using Curriculum.Models;

namespace Curriculum.Repositorys
{
    public interface ICourseRepository
    {
        List<Courses> GetCourseList();
        void PushCourse(Courses course);
        void DeleteCourse(string courseCode);
        void UpdateCourse(Courses course);
        List<Courses> SearchCourses(string search);

    }
}
