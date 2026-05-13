using AMS_Backend_V2.DTOs;
namespace AMS_Backend_V2.Services.AttendanceServe
{
    public interface IAttendanceServices
    {
        Task<IEnumerable<AttendanceDto.ReadAttendanceDto>> GetAllAttendancesAsync();
        Task<AttendanceDto.ReadAttendanceDto> GetByAttendanceIdAsync(int id);
        Task<bool> CreateAttendanceAsync(AttendanceDto.CreateAttendanceDto AttendanceDto);
        Task UpdateAttendanceAsync(AttendanceDto.UpdateAttentanceDto AttendanceDto);
        Task DeleteAttendanceAsync(int id);
    }
}
