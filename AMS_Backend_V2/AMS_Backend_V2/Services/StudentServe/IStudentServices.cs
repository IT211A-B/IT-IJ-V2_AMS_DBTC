using AMS_Backend_V2.DTOs;

namespace AMS_Backend_V2.Services.StudentServe
{
    public interface IStudentServices
    {
        Task<IEnumerable<StudentDto.ReadStudentDto>> GetAllStudentsAsync();
        Task<StudentDto.ReadStudentDto> GetByStudentIdAsync(int id);
        Task CreateStudentAsync(StudentDto.CreateStudentDto studentDto);
        Task UpdateStudentAsync(StudentDto.UpdateStudentDto studentDto);
        Task DeleteStudentAsync(int id);
    }
}
