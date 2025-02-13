using Curriculum.Repositorys;
using YourNamespace.Models;

namespace Curriculum.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository superHeroRepository)
        {
            _studentRepository = superHeroRepository;
        }

        public List<Students> GetStudentsList()
        {
            var result = _studentRepository.GetStudentsList();
            return result;
        }
        public void PushStudent(Students student) 
        {
            _studentRepository.PushStudent(student);
        }
        public void UpdateStudent(Students student)
        {
            _studentRepository.UpdateStudent(student);
        }
        public void DeleteStudent(string studentId)
        {
            _studentRepository.DeleteStudent(studentId);
        }
    }
}
