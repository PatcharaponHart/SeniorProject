namespace Curriculum.Models
{
    public class StudentGradeCourseDto
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CourseCode { get; set; }
        public string CourseNameTH { get; set; }
        public string CourseNameEN { get; set; }
        public string SubjectGroup { get; set; }
        public string Grade { get; set; }
        public int Credit { get; set;}
    }
}
