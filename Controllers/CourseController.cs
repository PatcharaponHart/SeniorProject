using Curriculum.Models;
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
    public class CourseController : ControllerBase
    {
        private readonly CurriculumDbContext _context;

        private readonly ICourseService _courseService;

        public CourseController(CurriculumDbContext context, ICourseService courseService)
        {
            _context = context;
            _courseService = courseService;
        }

        [HttpGet("GetCoursesList")]
        public ActionResult GetCoursesList()
        {
            var result = _courseService.GetCourseList();
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<Courses>> GetCourse(string courseCode)
        {
            var course = await _context.tblCourses.FindAsync(courseCode);
            if (course is null)
                return NotFound("Course not found.");

            return Ok(course);
        }

        [HttpPost("PushCourse")]
        public IActionResult PushCourse([FromBody] Courses course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Course data");
            }
            _courseService.PushCourse(course);
            return Ok("Course added Successfully");
        }
        
        [HttpPut("UpdateCourse")]
        public ActionResult UpdateCourse(Courses course)
        {
            _courseService.UpdateCourse(course);
            return Ok("Course Update Successfully");
        }

        [HttpDelete("DeleteCourse")]
        public ActionResult DeleteStudent(string courseCode)
        {
            _courseService.DeleteCourse(courseCode);
            return Ok("Course delete Successfully");
        }
    }
}
