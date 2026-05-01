using AMS_Backend_V2.Data;
using AMS_Backend_V2.Models;
using Microsoft.EntityFrameworkCore;
namespace AMS_Backend_V2.Repositories.CourseRepo
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AttendanceDbContext _context;
        public CourseRepository(AttendanceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }
        public async Task<Course> GetByCourseIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }
        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null) 
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
