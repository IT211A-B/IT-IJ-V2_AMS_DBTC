using AMS_Backend_V2.Data;
using AMS_Backend_V2.Models;
using Microsoft.EntityFrameworkCore;
namespace AMS_Backend_V2.Repositories.AttendanceRepo
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AttendanceDbContext _context;
        public AttendanceRepository(AttendanceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Attendance>> GetAllAttendancesAsync()
        {
            return await _context.Attendances.ToListAsync();
        }
        public async Task<Attendance> GetByAttendanceIdAsync(int id)
        {
            return await _context.Attendances.FindAsync(id);
        }
        public async Task AddAttendanceAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAttendanceAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }
        }
    }
}
