using AMS_Backend_V2.DTOs;
using AMS_Backend_V2.Models;
using AMS_Backend_V2.Repositories.CourseRepo;
namespace AMS_Backend_V2.Services.CourseServe
{
    public class CourseService : ICourseServices
    {
        private readonly ICourseRepository _courseRepo;
        public  CourseService(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }
        public async Task<IEnumerable<CourseDto.ReadCourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepo.GetAllCoursesAsync();
            return courses.Select(s => new CourseDto.ReadCourseDto
            {
                CourseId = s.CourseId,
                CourseCode = s.CourseCode,
                Description = s.Description,
            }).ToList();
        }
        public async Task<CourseDto.ReadCourseDto> GetByCourseIdAsync(int id)
        {
            var courses = await _courseRepo.GetByCourseIdAsync(id);
            if (courses == null)
            {
                return null;
            }
            return new CourseDto.ReadCourseDto
            {
                CourseId = courses.CourseId,
                CourseCode = courses.CourseCode,
                Description = courses.Description
            };
        }
        public async Task CreateCourseAsync(CourseDto.CreateCourseDto courseDto)
        {
            var courses = new Course
            {
                CourseCode = courseDto.CourseCode,
                Description = courseDto.Description
            };
            await _courseRepo.AddCourseAsync(courses);
        }
        public async Task<bool> UpdateCourseAsync(CourseDto.UpdateCourseDto courseDto)
        {
            var eCourse = await _courseRepo.GetByCourseIdAsync(courseDto.CourseId);
            if (eCourse == null)
            {
                return false;
            }
            eCourse.CourseId = courseDto.CourseId;
            eCourse.CourseCode = courseDto.CourseCode;
            eCourse.Description = courseDto.Description;
            await _courseRepo.UpdateCourseAsync(eCourse);
            return true;
        }
        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _courseRepo.GetByCourseIdAsync(id);
            if (course == null)
            {
                return false;
            }
            await _courseRepo.DeleteCourseAsync(id);
            return true;
        }
    }
}
