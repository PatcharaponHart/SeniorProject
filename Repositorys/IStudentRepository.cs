using YourNamespace.Models;

namespace Curriculum.Repositorys
{
    public interface IStudentRepository
    {
        public List<Students> GetStudentsList();
        void PushStudent(Students student);
        void UpdateStudent(Students student);
        void DeleteStudent(string studentId);
    }
}
