using Curriculum.Models;
using Curriculum.Service;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Data;

namespace Curriculum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly CurriculumDbContext _context;

        private readonly IGradeService _gredeService;

        public GradeController(CurriculumDbContext context, IGradeService gredeService)
        {
            _context = context;
            _gredeService = gredeService;
        }
        [HttpGet("GetGradeList")]
        public ActionResult GetGradeList()
        {
            var result = _gredeService.GetGradeList();
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<Grades>> GetGrade(string studentId, string courseCode)
        {
            var grade = await _context.tblGrades.FindAsync(studentId, courseCode);
            if (grade is null)
                return NotFound("Grade not found.");

            return Ok(grade);
        }

        [HttpPost("PushGrade")]
        public IActionResult PushGrade([FromBody] Grades grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Grade data");
            }
            _gredeService.PushGrade(grade);
            return Ok("Grade added Successfully");
        }

        [HttpPut("UpdateGrade")]
        public ActionResult UpdateGrade(Grades grade)
        {
            _gredeService.UpdateGrade(grade);
            return Ok("Grade Update Successfully");
        }

        //[HttpDelete("DeleteGrade")]
        //public ActionResult DeleteGrade(string courseCode)
        //{
        //    _gredeService.DeleteGrade(courseCode);
        //    return Ok("Course delete Successfully");
        //}
    }
}
