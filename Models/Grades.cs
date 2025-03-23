using System.ComponentModel.DataAnnotations;

namespace Curriculum.Models
{
    public class Grades
    {
        public string StudentID { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string Grade {  get; set; } 
        public int Academic_year { get; set; }
        public int Semester {  get; set; }
        public string Credit {  get; set; }

    }
}
