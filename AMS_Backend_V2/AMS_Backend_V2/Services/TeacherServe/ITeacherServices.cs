using AMS_Backend_V2.DTOs;

namespace AMS_Backend_V2.Services.TeacherServe
{
    public interface ITeacherServices
    {
        Task<IEnumerable<TeacherDto.ReadTeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto.ReadTeacherDto> GetByTeacherIdAsync(int id);
        Task CreateTeacherAsync(TeacherDto.CreateTeacherDto teacherDto);
        Task UpdateTeacherAsync(TeacherDto.UpdateTeacherDto teacherDto);
        Task DeleteTeacherAsync(int id);
    }
}
