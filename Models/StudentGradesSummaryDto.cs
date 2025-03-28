namespace Curriculum.Models
{
    public class StudentGradesSummaryDto
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<StudentGradeCourseDto> Grades { get; set; }
        public decimal TotalCredits { get; set; }
    }
}
