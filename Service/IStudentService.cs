using YourNamespace.Models;

namespace Curriculum.Service
{
    public interface IStudentService
    {
        List<Students> GetStudentsList();
        void PushStudent(Students student);
        void UpdateStudent(Students student);
        void DeleteStudent(string studentId);
    }

}
