using AMS_Backend_V2.Enums;

namespace AMS_Backend_V2.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public string Email { get; set; }

    }
}
