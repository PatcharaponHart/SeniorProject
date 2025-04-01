using Curriculum.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data;
using YourNamespace.Models;

namespace Curriculum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CurriculumDbContext _context;

        private readonly IStudentService _studentService;

        public StudentController(CurriculumDbContext context,IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }

        [HttpGet("GetStudentsList")]
        public ActionResult GetStudentsList()
        {
            var result = _studentService.GetStudentsList();
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<Students>> GetStudent(string studentId)
        {
            var student = await _context.tblStudents.FindAsync(studentId);
            if (student is null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        [HttpPost("PushStudent")]
        public IActionResult PushStudent([FromBody] Students student)
        {
            // การตรวจสอบ ModelState ยังคงเดิม
            if (!ModelState.IsValid)
            {
                // อาจจะส่งรายละเอียด Error กลับไปได้ ถ้าต้องการ
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                // return BadRequest(new { message = "Invalid Student data", errors = errors });
                return BadRequest("Invalid Student data"); // หรือแบบเดิม
            }

            try // <<< เพิ่ม try ครอบส่วนที่เรียก Service
            {
                // เรียก Service ซึ่งอาจจะ throw InvalidOperationException ถ้าข้อมูลซ้ำ
                _studentService.PushStudent(student);

                // ถ้าโค้ดทำงานมาถึงตรงนี้ได้ แสดงว่าไม่เกิด Exception และเพิ่มข้อมูลสำเร็จ
                return Ok("Student added Successfully");
            }
            catch (InvalidOperationException ex) // <<< เพิ่ม catch เพื่อดักจับ Exception ข้อมูลซ้ำโดยเฉพาะ
            {
                // เมื่อ Service โยน InvalidOperationException (เพราะข้อมูลซ้ำ)
                // ให้ส่ง HTTP 409 Conflict กลับไป พร้อมข้อความจาก Exception
                // Frontend จะได้รับ Status 409 และสามารถแสดง ex.Message ใน Toast ได้
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex) // <<< (แนะนำ) เพิ่ม catch ดักจับ Exception ทั่วไปอื่นๆ ที่อาจเกิดขึ้น
            {
                // ควร Log รายละเอียดของ Exception นี้ไว้เสมอ สำหรับ Debug
                // ตัวอย่าง Log ง่ายๆ ไปที่ Output Window ตอน Debug
                System.Diagnostics.Debug.WriteLine($"!!! UNEXPECTED ERROR in PushStudent: {ex.ToString()}");

                // ส่ง HTTP 500 Internal Server Error กลับไป สำหรับข้อผิดพลาดที่ไม่คาดคิดอื่นๆ
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred on the server." });
            }
        }
        //[HttpPut]
        //public async Task<ActionResult<List<Students>>> UpdateStudent(Students updateStudent)
        //{
        //    var dbStudent = await _context.tblStudents.FindAsync(updateStudent.StudentID);
        //    if (dbStudent is null)
        //        return NotFound("Student not found.");

        //    dbStudent.FirstName = updateStudent.FirstName;
        //    dbStudent.LastName = updateStudent.LastName;
        //    dbStudent.Username = updateStudent.Username;
        //    dbStudent.Password = updateStudent.Password;

        //    await _context.SaveChangesAsync();

        //    return Ok(await _context.tblStudents.ToListAsync());
        //}

        [HttpPut("UpdateStudent")]
        public ActionResult UpdateStudent(Students student)
        {
            _studentService.UpdateStudent(student);
            return Ok("Student Update Successfully");
        }

        [HttpDelete("DeleteStudent")]
        public ActionResult DeleteStudent(string studentId)
        {
            _studentService.DeleteStudent(studentId);
            return Ok("Student delete Successfully");
        }
    }
}
