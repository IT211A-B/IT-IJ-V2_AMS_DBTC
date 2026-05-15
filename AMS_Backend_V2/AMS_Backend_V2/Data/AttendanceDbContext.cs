using Microsoft.EntityFrameworkCore;
using AMS_Backend_V2.Models;

namespace AMS_Backend_V2.Data
{
    public class AttendanceDbContext : DbContext
    {
        public AttendanceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
       
    }
}
