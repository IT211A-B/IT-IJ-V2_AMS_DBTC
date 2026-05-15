using AMS_Backend_V2.Models;

namespace AMS_Backend_V2.Repositories.TeacherRepo
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetByTeacherIdAsync(int id);
        Task AddTeacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
    }
}
