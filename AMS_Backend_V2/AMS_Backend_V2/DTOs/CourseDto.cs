using System.ComponentModel.DataAnnotations;
namespace AMS_Backend_V2.DTOs
{
    public class CourseDto
    {
        public class CreateCourseDto
        {
            [Required] public string CourseCode { get; set; }
            [Required] public string Description { get; set; }
        }

        public class ReadCourseDto
        {
            public int CourseId { get; set; }
            public string CourseCode { get; set; }
            public string Description { get; set; }
        }
        public class UpdateCourseDto
        {
            [Required] public string CourseCode { get; set; }
            [Required] public string Description { get; set; }
        }

    }
}
