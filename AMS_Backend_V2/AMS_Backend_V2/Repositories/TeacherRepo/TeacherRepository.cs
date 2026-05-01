using AMS_Backend_V2.Data;
using AMS_Backend_V2.Models;
using Microsoft.EntityFrameworkCore;
namespace AMS_Backend_V2.Repositories.TeacherRepo
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AttendanceDbContext _context;
        public TeacherRepository (AttendanceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync() 
        {
            return await _context.Teachers.ToListAsync();
        }
        public async Task<Teacher> GetByTeacherIdAsync(int id)
        {
            return await _context.Teachers.FindAsync();
        }
        public async Task AddTeacherAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
        }
        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            } 

        }
    }
}
