using AMS_Backend_V2.Enums;
using System.ComponentModel.DataAnnotations;
namespace AMS_Backend_V2.DTOs
{
    public class StudentDto
    {
        public class CreateStudentDto
        {
            [Required] public string FirstName { get; set; }
            [Required] public string LastName { get; set; }
            [Required] public Sex Sex { get; set; } 
            [Required] public string Email { get; set; }
        }

        public class ReadStudentDto
        {
            public int StudentId { get; set; }
            public string FullName { get; set; }
            public Sex Sex { get; set; }
            public string Email { get; set; }   

        }
        public class UpdateStudentDto
        {
            [Required] public string FirstName { get; set; }
            [Required] public string LastName { get; set; }
            [Required] public Sex Sex { get; set; }
            [Required] public string Email { get; set; }
        }
    }
}
