using AMS_Backend_V2.DTOs;
using AMS_Backend_V2.Models;

namespace AMS_Backend_V2.Services.CourseServe
{
    public interface ICourseServices
    {
        Task<IEnumerable<CourseDto.ReadCourseDto>> GetAllCoursesAsync();
        Task<CourseDto.ReadCourseDto> GetByCourseIdAsync(int id);
        Task CreateCourseAsync(CourseDto.CreateCourseDto courseDto);
        Task UpdateCourseAsync(CourseDto.UpdateCourseDto courseDto);
        Task DeleteCourseAsync(int id);
    }
}
