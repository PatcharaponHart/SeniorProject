using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Students
    {
        [Key]
        public string StudentID { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Section { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
