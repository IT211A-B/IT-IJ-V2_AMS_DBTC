using AMS_Backend_V2.Enums;
using System.ComponentModel.DataAnnotations;
namespace AMS_Backend_V2.DTOs
{
    public class TeacherDto
    {
        public class CreateTeacherDto
        {
            [Required] public string FirstName { get; set; }
            [Required] public string LastName { get; set; }
            [Required] public Sex Sex { get; set; }
            [Required] public string Email { get; set; }
        }

        public class ReadTeacherDto
        {
            public int TeacherId { get; set; }
            public string FullName { get; set; }
            public Sex Sex { get; set; }
            public string Email { get; set; }

        }
        public class UpdateTeacherDto
        {
            [Required] public int TeacherId { get; set; }
            [Required] public string FirstName { get; set; }
            [Required] public string LastName { get; set; }
            [Required] public Sex Sex { get; set; }
            [Required] public string Email { get; set; }
        }
    }
}
