using AMS_Backend_V2.Models;
using AMS_Backend_V2.DTOs;
using AMS_Backend_V2.Enums;
using AMS_Backend_V2.Repositories.StudentRepo;
using System.ComponentModel.DataAnnotations;
namespace AMS_Backend_V2.Services.StudentServe
{
    public class StudentService : IStudentServices
    {
        private readonly IStudentRepository _studentRepo;
        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }
        public async Task<IEnumerable<StudentDto.ReadStudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepo.GetAllStudentsAsync();
            return students.Select(s => new StudentDto.ReadStudentDto
            {
                StudentId = s.StudentId,
                FullName = s.FirstName + " " + s.LastName
            }).ToList();
        }
        public async Task<StudentDto.ReadStudentDto> GetByStudentIdAsync(int id)
        {
            var students = await _studentRepo.GetByStudentIdAsync(id);
            if (students == null)
            {
                return null;
            }
            return new StudentDto.ReadStudentDto
            {
                StudentId = students.StudentId,
                FullName = students.FirstName + " " + students.LastName
            };
        }
        public async Task CreateStudentAsync(StudentDto.CreateStudentDto studentDto)
        {
            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Sex = studentDto.Sex,
                Email = studentDto.Email
            };
            await _studentRepo.AddStudentAsync(student);
        }
        public async Task UpdateStudentAsync(StudentDto.UpdateStudentDto studentDto)
        {
            var eStudent = await _studentRepo.GetByStudentIdAsync(studentDto.StudentId);
            if (eStudent != null)
            {
                eStudent.FirstName = studentDto.FirstName;
                eStudent.LastName = studentDto.LastName;
                eStudent.Sex = studentDto.Sex;
                eStudent.Email = studentDto.Email;
                await _studentRepo.UpdateStudentAsync(eStudent);
            }
        }
        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepo.DeleteStudentAsync(id);
        }
    }
}
