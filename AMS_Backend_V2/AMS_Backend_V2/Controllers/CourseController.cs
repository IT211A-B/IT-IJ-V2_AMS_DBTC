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

        /// <summary>
        /// Retrieves a complete list of all courses.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseServices.GetAllCoursesAsync();
            return Ok(courses);
        }

        /// <summary>
        /// Retrieves the details of a specific course by their unique ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCourseId(int id)
        {
            var course = await _courseServices.GetByCourseIdAsync(id);
            if (course == null) return NotFound($"Course with ID {id} not found");
            return Ok(course);
        }

        /// <summary>
        /// Registers a new course into the system.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseDto.CreateCourseDto courseDto)
        {
            await _courseServices.CreateCourseAsync(courseDto);
            return Ok("Course Successfully Created");
        }

        /// <summary>
        /// Updates an existing course's information.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateCourse(CourseDto.UpdateCourseDto courseDto)
        {
            var update = await _courseServices.UpdateCourseAsync(courseDto);
            if (!update) return NotFound("Update Failed: Course Not Found");
            return Ok("Course Updated Successfully");
        }

        /// <summary>
        /// Removes a course record from the system.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var delete = await _courseServices.DeleteCourseAsync(id);
            if (!delete) return NotFound("Delete Failed: Course Not Found");
            return Ok("Course Succesfully Deleted");
        }
    }
}
