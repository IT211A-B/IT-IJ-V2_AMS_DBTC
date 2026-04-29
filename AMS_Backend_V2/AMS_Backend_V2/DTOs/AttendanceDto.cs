using AMS_Backend_V2.Enums;
using System.ComponentModel.DataAnnotations;
namespace AMS_Backend_V2.DTOs
{
    public class AttendanceDto
    {
        public class CreateAttendanceDto
        {
            [Required] public int StudentId { get; set; }
            [Required] public int CourseId { get; set; }
            [Required] public DateTime Date { get; set; }
            [Required] public Status Status { get; set; }
        }

        public class ReadAttendanceDto
        {
            public int AttendanceId { get; set; }
            public int StudentId { get; set; }
            public int CourseId { get; set; }
            public DateTime Date { get; set; }
            public Status Status { get; set; }
        }
        
        public class UpdateAttentanceDto
        {
            [Required] public int StudentId { get; set; }
            [Required] public int CourseId { get; set; }
            [Required] public DateTime Date { get; set; }
            [Required] public Status Status { get; set; }
        }

    }
}
