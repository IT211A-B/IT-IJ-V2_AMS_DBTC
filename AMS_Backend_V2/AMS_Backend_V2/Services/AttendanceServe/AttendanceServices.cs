using AMS_Backend_V2.DTOs;
using AMS_Backend_V2.Enums;
using AMS_Backend_V2.Models;
using AMS_Backend_V2.Repositories.AttendanceRepo;
using AMS_Backend_V2.Repositories.StudentRepo;
using AMS_Backend_V2.Services.AttendanceServe;
using System.ComponentModel.DataAnnotations;

namespace AMS_Backend_V2.Services.StudentServe
{
    public class AttendanceService : IAttendanceServices
    {
        private readonly IAttendanceRepository _attendanceRepo;
        public AttendanceService(IAttendanceRepository attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }
        public async Task<IEnumerable<AttendanceDto.ReadAttendanceDto>> GetAllAttendancesAsync()
        {
            var attendance = await _attendanceRepo.GetAllAttendancesAsync();
            return attendance.Select(s => new AttendanceDto.ReadAttendanceDto
            {
                AttendanceId = s.AttendanceId,
                StudentId = s.StudentId,
                CourseId = s.CourseId,
                Date = s.Date,
                Status = s.Status,  
            }).ToList();
        }
        public async Task<AttendanceDto.ReadAttendanceDto> GetByAttendanceIdAsync(int id)
        {
            var attendance = await _attendanceRepo.GetByAttendanceIdAsync(id);
            if (attendance == null)
            {
                return null;
            }
            return new AttendanceDto.ReadAttendanceDto
            {
                AttendanceId = attendance.AttendanceId,
                StudentId = attendance.StudentId,
                CourseId = attendance.CourseId,
                Date = attendance.Date,
                Status = attendance.Status,
            };
        }
        public async Task CreateAttendanceAsync(AttendanceDto.CreateAttendanceDto attendanceDto)
        {
            var attendance = new Attendance
            {
                StudentId = attendanceDto.StudentId,
                CourseId = attendanceDto.CourseId,
                Date = attendanceDto.Date,
                Status = attendanceDto.Status,
            };
            await _attendanceRepo.AddAttendanceAsync(attendance);
        }
        public async Task UpdateAttendanceAsync(AttendanceDto.UpdateAttentanceDto attendanceDto)
        {
            var eAttendance = await _attendanceRepo.GetByAttendanceIdAsync(attendanceDto.AttendanceId);
            if (eAttendance != null)
            {
                eAttendance.StudentId = attendanceDto.StudentId;
                eAttendance.CourseId = attendanceDto.CourseId;
                eAttendance.Date = attendanceDto.Date;
                eAttendance.Status = attendanceDto.Status;
                await _attendanceRepo.UpdateAttendanceAsync(eAttendance);
            }
        }
        public async Task DeleteAttendanceAsync(int id)
        {
            await _attendanceRepo.DeleteAttendanceAsync(id);
        }
    }
}
