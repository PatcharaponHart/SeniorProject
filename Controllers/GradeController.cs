﻿using Curriculum.Models;
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

        private readonly IGradeService _gradeService;

        public GradeController(CurriculumDbContext context, IGradeService gradeService)
        {
            _context = context;
            _gradeService = gradeService;
        }
        [HttpGet("GetGradeByStudentId")]
        public async Task<IActionResult> GetStudentGradesByStudentId(string studentId)
        {
            var result = await _gradeService.GetStudentGradesByStudentId(studentId);

            // ส่งคืนผลลัพธ์ในรูปแบบของ Ok ที่สามารถใช้งานใน API
            return Ok(result);
        }

        [HttpGet("grades-with-courses")]
        public async Task<ActionResult<IEnumerable<StudentGradeCourseDto>>> GetStudentGradesWithCourses()
        {
            var result = await _gradeService.GetStudentGradesWithCourseDetailsAsync();
            return Ok(result);
        }
        [HttpGet("GetGradeList")]
        public ActionResult GetGradeList()
        {
            var result = _gradeService.GetGradeList();
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
            _gradeService.PushGrade(grade);
            return Ok("Grade added Successfully");
        }
        [HttpPost("PushGrades")]
        public IActionResult PushGrades(List<Grades> grades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Grade data");
            }
            _gradeService.PushGrades(grades);
            return Ok("Grade added Successfully");
        }

        [HttpPut("UpdateGrade")]
        public ActionResult UpdateGrade(Grades grade)
        {
            _gradeService.UpdateGrade(grade);
            return Ok("Grade Update Successfully");
        }
        [HttpPut("UpdateEdit")]
        public IActionResult EditGrade(Grades grade)
        {
            _gradeService.UpdateGradeByStudent(grade);
            return Ok("Grade Update Successfully");
        }
        [HttpPut("UpdateGrades")]
        public IActionResult UpdateGrades(List<Grades> gradesList)
        {
            _gradeService.UpdateGrades(gradesList);
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
