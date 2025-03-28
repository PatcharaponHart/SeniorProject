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
        public IActionResult PushStudent([FromBody]Students student) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Student data");
            }
            _studentService.PushStudent(student);
            return Ok("Student added Successfully");
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
