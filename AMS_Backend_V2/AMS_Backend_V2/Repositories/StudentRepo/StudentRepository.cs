using AMS_Backend_V2.Data;
using AMS_Backend_V2.Models;
using Microsoft.EntityFrameworkCore;
namespace AMS_Backend_V2.Repositories.StudentRepo
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AttendanceDbContext _context;
        public StudentRepository(AttendanceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync();
        }
        public async Task AddAsync(Student Student)
        {
            await _context.Students.AddAsync(Student);
        }
        public async Task UpdateAsync(Student Student)
        {
            _context.Students.Update(Student);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
