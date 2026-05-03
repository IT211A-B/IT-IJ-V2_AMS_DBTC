using AMS_Backend_V2.Services.CourseServe;
using AMS_Backend_V2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AMS_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;
        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseServices.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCourseId(int id)
        {
            var course = await _courseServices.GetByCourseIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseDto.CreateCourseDto courseDto)
        {
            await _courseServices.CreateCourseAsync(courseDto);
            return Ok("Course Successfully Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(CourseDto.UpdateCourseDto courseDto)
        {
            await _courseServices.UpdateCourseAsync(courseDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseServices.DeleteCourseAsync(id);
            return Ok("Course Succesfully Deleted");
        }
    }
}
