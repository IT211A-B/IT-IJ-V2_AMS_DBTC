using AMS_Backend_V2.DTOs;
using AMS_Backend_V2.Enums;
using AMS_Backend_V2.Models;
using AMS_Backend_V2.Repositories.TeacherRepo;

namespace AMS_Backend_V2.Services.TeacherServe
{
    public class TeacherService : ITeacherServices
    {
        private readonly ITeacherRepository _teacherRepo;
        public TeacherService(ITeacherRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }
        public async Task<IEnumerable<TeacherDto.ReadTeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepo.GetAllTeachersAsync();
            return teachers.Select(s => new TeacherDto.ReadTeacherDto
            {
                TeacherId = s.TeacherId,
                FullName = s.FirstName + " " + s.LastName,
                Sex = s.Sex,
                Email = s.Email,
            }).ToList();
        }
        public async Task<TeacherDto.ReadTeacherDto> GetByTeacherIdAsync(int id)
        {
            var teachers = await _teacherRepo.GetByTeacherIdAsync(id);
            if (teachers == null)
            {
                return null;
            }
            return new TeacherDto.ReadTeacherDto
            {
                TeacherId = teachers.TeacherId,
                FullName = teachers.FirstName + " " + teachers.LastName,
                Sex = teachers.Sex,
                Email = teachers.Email,
            };
        }
        public async Task CreateTeacherAsync(TeacherDto.CreateTeacherDto teacherDto)
        {
            var teacher = new Teacher
            {
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                Sex = teacherDto.Sex,
                Email = teacherDto.Email,
            };
            await _teacherRepo.AddTeacherAsync(teacher);
        }
        public async Task<bool> UpdateTeacherAsync(TeacherDto.UpdateTeacherDto teacherDto)
        {
            var eTeacher = await _teacherRepo.GetByTeacherIdAsync(teacherDto.TeacherId);
            if (eTeacher == null)
            {
                return false;
            }
            eTeacher.FirstName = teacherDto.FirstName;
            eTeacher.LastName = teacherDto.LastName;
            eTeacher.Sex = teacherDto.Sex;
            eTeacher.Email = teacherDto.Email;
            await _teacherRepo.UpdateTeacherAsync(eTeacher);
            return true;
        }
        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var student = await _teacherRepo.GetByTeacherIdAsync(id);
            if (student == null)
            {
                return false;
            }
            await _teacherRepo.DeleteTeacherAsync(id);
            return true;
        }
    }
}