using AMS_Backend_V2.Services.StudentServe;
using AMS_Backend_V2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AMS_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        /// <summary>
        /// Retrieves a complete list of all students.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentServices.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// Retrieves the details of a specific student by their unique ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByStudentId(int id)
        {
            var students = await _studentServices.GetByStudentIdAsync(id);
            if (students == null) return NotFound($"Student with ID {id} not found");
            return Ok(students);
        }

        /// <summary>
        /// Registers a new student into the system.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentDto.CreateStudentDto studentDto)
        {
            await _studentServices.CreateStudentAsync(studentDto);
            return Ok("Student Created Successfully");
        }

        /// <summary>
        /// Updates an existing student's information.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentDto.UpdateStudentDto studentDto)
        {
            var update = await _studentServices.UpdateStudentAsync(studentDto);
            if (!update) return NotFound("Update Failed: Student Not Found");
            return Ok("Student Updated Successfully");
        }

        /// <summary>
        /// Removes a student record from the system
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var delete = await _studentServices.DeleteStudentAsync(id);
            if (!delete) return NotFound("Delete Failed: Student Not Found");
            return Ok("Student Deleted Successfully");
        }
    }
}
