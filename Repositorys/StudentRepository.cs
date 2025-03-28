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
            // --- เพิ่มการตรวจสอบข้อมูลซ้ำ ---
            // ตรวจสอบว่ามี Username นี้อยู่แล้วหรือไม่
            bool usernameExists = _context.tblStudents.Any(s => s.Username == student.Username);
            if (usernameExists)
            {
                // ถ้ามีอยู่แล้ว ให้โยน Exception หรือ Return ค่าที่บ่งบอกถึงข้อผิดพลาด
                // การโยน Exception จะทำให้ Controller จับไป xử lý ต่อได้ง่าย
                throw new InvalidOperationException("Username already exists.");
            }

            // ตรวจสอบว่ามี StudentID นี้อยู่แล้วหรือไม่
            bool studentIdExists = _context.tblStudents.Any(s => s.StudentID == student.StudentID);
            if (studentIdExists)
            {
                throw new InvalidOperationException("Student ID already exists.");
            }
            // --- สิ้นสุดการตรวจสอบ ---

            // ถ้าไม่ซ้ำ ก็ทำการเพิ่มข้อมูลตามปกติ
            // อาจจะต้องมีการ Hash Password ก่อนบันทึกจริง
            // student.Password = HashPassword(student.Password); // ตัวอย่างการ Hash
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
