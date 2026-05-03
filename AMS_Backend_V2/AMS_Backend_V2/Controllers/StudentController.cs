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

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentServices.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByStudentId(int id)
        {
            var students = await _studentServices.GetByStudentIdAsync(id);
            if (students == null) return NotFound();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentDto.CreateStudentDto studentDto)
        {
            await _studentServices.CreateStudentAsync(studentDto);
            return Ok("Student Created Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentDto.UpdateStudentDto studentDto)
        {
            await _studentServices.UpdateStudentAsync(studentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentServices.DeleteStudentAsync(id);
            return Ok("Student Deleted Successfully");
        }
    }
}
