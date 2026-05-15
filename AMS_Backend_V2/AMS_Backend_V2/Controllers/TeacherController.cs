using AMS_Backend_V2.Services.TeacherServe;
using AMS_Backend_V2.DTOs;
using Microsoft.AspNetCore.Mvc;
using AMS_Backend_V2.Services.StudentServe;

namespace AMS_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherServices _teacherServices;
        public TeacherController(ITeacherServices teacherServices)
        {
            _teacherServices = teacherServices;
        }

        /// <summary>
        /// Retrieves a complete list of all teachers.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherServices.GetAllTeachersAsync();
            return Ok(teachers);
        }

        /// <summary>
        /// Retrieves the details of a specific teacher by their unique ID.
        /// </summary>
        [HttpGet("{id}")]

        public async Task<IActionResult> GetByTeacherId(int id)
        {
            var teachers = await _teacherServices.GetByTeacherIdAsync(id);
            if (teachers == null) return NotFound($"Teacher with ID {id} not found");
            return Ok(teachers);
        }

        /// <summary>
        /// Registers a new teacher into the system.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(TeacherDto.CreateTeacherDto teacherDto)
        {
            await _teacherServices.CreateTeacherAsync(teacherDto);
            return Ok("Teacher Created Successfully");
        }

        /// <summary>
        /// Updates an existing teacher's information.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(TeacherDto.UpdateTeacherDto teacherDto)
        {
            var update = await _teacherServices.UpdateTeacherAsync(teacherDto);
            if (!update) return NotFound("Update Failed: Teacher Not Found");
            return Ok("Teacher Updated Successfully");
        }

        /// <summary>
        /// Removes a teacher record from the system.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var delete = await _teacherServices.DeleteTeacherAsync(id);
            if (!delete) return NotFound("Delete Failed: Teacher Not Found");
            return Ok("Teacher Deleted Successfully");
        }
    }
}
