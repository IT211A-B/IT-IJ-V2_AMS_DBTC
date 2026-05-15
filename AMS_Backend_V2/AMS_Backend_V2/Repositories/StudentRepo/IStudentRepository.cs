using AMS_Backend_V2.Models;

namespace AMS_Backend_V2.Repositories.StudentRepo
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetByStudentIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }
}
