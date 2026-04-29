using AMS_Backend_V2.Enums;
namespace AMS_Backend_V2.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
    }
}
