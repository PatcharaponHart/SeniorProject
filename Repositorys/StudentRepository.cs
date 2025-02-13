using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using YourNamespace.Data;
using YourNamespace.Models;

namespace Curriculum.Repositorys
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CurriculumDbContext _context;

        public StudentRepository(CurriculumDbContext context)
        {
            _context = context;
        }

        public List<Students> GetStudentsList()
        {

            var result = _context.tblStudents.ToList();

            return result;
        }
        public void PushStudent(Students student)
        {
            _context.tblStudents.Add(student);
            _context.SaveChanges();
        }
        public void UpdateStudent(Students student)
        {
            var updateStudent = _context.tblStudents.Find(student.StudentID);
            updateStudent.FirstName = student.FirstName;
            updateStudent.LastName = student.LastName;
            updateStudent.Username = student.Username;

            _context.SaveChanges();
        }
        public void DeleteStudent(string studentId)
        {
            var studentToDelete = _context.tblStudents.Find(studentId);
            if (studentToDelete != null)
            {
                _context.tblStudents.Remove(studentToDelete);
                _context.SaveChanges();
            }
        }
    }
}
