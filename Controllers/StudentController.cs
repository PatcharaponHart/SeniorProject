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
            if (!ModelState.IsValid)
            {
                // รวม Error Messages จาก ModelState (ถ้าต้องการแสดงรายละเอียดมากขึ้น)
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Invalid Student data", errors = errors });
                // หรือ return BadRequest("Invalid Student data"); แบบเดิมก็ได้
            }

            try
            {
                _studentService.PushStudent(student);
                return Ok("Student added Successfully");
            }
            catch (InvalidOperationException ex) // ดักจับ Exception ที่โยนมาจาก Service
            {
                // ถ้าเป็น Exception เกี่ยวกับข้อมูลซ้ำ ให้ส่ง 409 Conflict หรือ 400 Bad Request กลับไป
                // Status 409 Conflict เหมาะสมกว่าสำหรับการแจ้งว่าข้อมูลขัดแย้งกับที่มีอยู่แล้ว
                return Conflict(new { message = ex.Message }); // ส่ง Message จาก Exception กลับไป
                                                               // หรือจะส่งเป็น BadRequest ก็ได้:
                                                               // return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // ดักจับ Error อื่นๆ ที่อาจเกิดขึ้น
            {
                System.Diagnostics.Debug.WriteLine($"!!! UNEXPECTED ERROR in PushStudent: {ex.Message}");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                // Log error จริงๆ ไว้ด้วย
                // Log.Error(ex, "Error pushing student");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

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
