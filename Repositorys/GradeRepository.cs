using Curriculum.Models;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data;

namespace Curriculum.Repositorys
{
    public class GradeRepository : IGradeRepository
    {
        private readonly CurriculumDbContext _context;
        public GradeRepository(CurriculumDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StudentGradeCourseDto>> GetStudentGradesWithCourseDetailsAsync()
        {
            // LINQ Method Syntax
            var result = await _context.tblStudents
                .Join(_context.tblGrades,
                    student => student.StudentID,
                    grade => grade.StudentID,
                    (student, grade) => new { Student = student, Grade = grade })
                .Join(_context.tblCourses,
                    sg => sg.Grade.CourseCode,
                    course => course.CourseCode,
                    (sg, course) => new StudentGradeCourseDto
                    {
                        StudentID = sg.Student.StudentID,
                        FirstName = sg.Student.FirstName,
                        LastName = sg.Student.LastName,
                        CourseCode = course.CourseCode,
                        CourseNameTH = course.CourseNameTH,
                        CourseNameEN = course.CourseNameEN,
                        Grade = sg.Grade.Grade,
                        //Academic_year = sg.Grade.Academic_year,
                        //Semester = sg.Grade.Semester
                    })
                .ToListAsync();

            return result;
        }
        public List<Grades> GetGradeList()
        {

            var result = _context.tblGrades.ToList();

            return result;
        }

        public void PushGrade(Grades grade)
        {
            _context.tblGrades.Add(grade);
            _context.SaveChanges();
        }
        public void PushGrade(string studentId, string courseCode, string grade, string credit)
        {
            var newGrade = new Grades
            {
                StudentID = studentId,
                CourseCode = courseCode,
                Grade = grade,
                Credit = credit,
                //Academic_year = DateTime.Now.Year, // หากต้องการใช้ปีปัจจุบัน
                //Semester = DetermineSemester() // Method กำหนดเทอม
            };

            _context.tblGrades.Add(newGrade);
            _context.SaveChanges();
        }

        // Method เสริมเพื่อกำหนดเทอมอัตโนมัติ
        private int DetermineSemester()
        {
            int month = DateTime.Now.Month;
            return (month >= 8 && month <= 12) ? 1 : 2;
        }

        // Method สำหรับ Bulk Insert หลายเกรด
        public void PushMultipleGrades(List<Grades> grades)
        {
            _context.tblGrades.AddRange(grades);
            _context.SaveChanges();
        }

        public void UpdateGrade(Grades grade)
        {
            var updateGrade = _context.tblGrades.Find(grade.StudentID);
            updateGrade.CourseCode = grade.CourseCode;
            updateGrade.Grade = grade.Grade;
            //updateGrade.Academic_year = grade.Academic_year;
            //updateGrade.Semester = grade.Semester;
            updateGrade.Credit = grade.Credit;

            _context.SaveChanges();
        }
        public void UpdateGradeByStudent(Grades grade)
        {
            // ค้นหาเกรดที่ต้องการอัพเดทด้วย StudentID และ CourseCode
            var existingGrade = _context.tblGrades
                .FirstOrDefault(g =>
                    g.StudentID == grade.StudentID &&
                    g.CourseCode == grade.CourseCode);

            // ถ้าพบข้อมูล
            if (existingGrade != null)
            {
                // อัพเดทฟิลด์ที่ต้องการ
                existingGrade.Grade = grade.Grade;
                existingGrade.Credit = grade.Credit;
                // อาจเพิ่มฟิลด์อื่นๆ ตามต้องการ

                _context.SaveChanges();
            }
            else
            {
                // หากไม่พบข้อมูล อาจโยน Exception หรือจัดการตามเหมาะสม
                throw new Exception("ไม่พบข้อมูลเกรดที่ต้องการแก้ไข");
            }
        }
        public void UpdateGrades(List<Grades> gradesList)
        {
            foreach (var grade in gradesList)
            {
                // ค้นหาข้อมูลโดยใช้ทั้ง StudentID และ CourseCode
                var updateGrade = _context.tblGrades
                    .FirstOrDefault(g => g.StudentID == grade.StudentID && g.CourseCode == grade.CourseCode);

                // ถ้าพบข้อมูลให้ทำการอัปเดต
                if (updateGrade != null)
                {
                    updateGrade.Grade = grade.Grade;
                    updateGrade.Credit = grade.Credit;
                }
                else
                {
                    // ถ้าไม่พบข้อมูลที่ตรงกัน สามารถเลือกทำอะไรบางอย่างได้ เช่น สร้างข้อมูลใหม่
                    // หรือโยนข้อผิดพลาด
                    throw new Exception($"ไม่พบข้อมูลเกรดสำหรับ StudentID: {grade.StudentID} และ CourseCode: {grade.CourseCode}");
                }
            }

            // บันทึกการเปลี่ยนแปลงทั้งหมดในครั้งเดียว
            _context.SaveChanges();
        }
        public List<Grades> SearchGrades(string search)
        {

            var result = _context.tblGrades.Where(a =>
                a.StudentID.Contains(search) ||
                string.IsNullOrEmpty(search)).ToList();


            return result;
        }
        //public void DeleteGrade(string courseCode)
        //{
        //    var courseToDelete = _context.tblGrades.Find(courseCode);
        //    if (courseToDelete != null)
        //    {
        //        _context.tblCourses.Remove(courseToDelete);
        //        _context.SaveChanges();
        //    }
        //}
    }
}
