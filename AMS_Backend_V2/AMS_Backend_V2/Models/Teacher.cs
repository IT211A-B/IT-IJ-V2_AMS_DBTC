using AMS_Backend_V2.Enums;
namespace AMS_Backend_V2.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public string Email { get; set; }

    }
}
