using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Curriculum.Models
{
    public class Courses
    {
        [Key]
        public string CourseCode { get; set; } = string.Empty;
        public string CourseNameTH { get; set; } = string.Empty;
        public string CourseNameEN { get; set; } = string.Empty;
        public string Credit { get; set; } = string.Empty;
        public string SubjectCategory { get; set; } = string.Empty;
    }
}
