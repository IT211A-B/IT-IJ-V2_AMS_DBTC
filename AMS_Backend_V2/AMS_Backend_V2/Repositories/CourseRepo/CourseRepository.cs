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
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }
        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }
        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
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
