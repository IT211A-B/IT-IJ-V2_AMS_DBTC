using AMS_Backend_V2.Models;

namespace AMS_Backend_V2.Repositories.CourseRepo
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course> GetByCourseIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);

    }
}
