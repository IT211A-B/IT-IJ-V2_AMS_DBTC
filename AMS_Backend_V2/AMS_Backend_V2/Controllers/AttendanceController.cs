using AMS_Backend_V2.Services.AttendanceServe;
using AMS_Backend_V2.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace AMS_Backend_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceServices _attendanceServices;
        public AttendanceController(IAttendanceServices attendanceServices)
        {
            _attendanceServices = attendanceServices;
        }

        /// <summary>
        /// Retrieves a complete list of all attendances.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceServices.GetAllAttendancesAsync();
            return Ok(attendances);
        }

        /// <summary>
        /// Retrieves the details of a specific attendance by their unique ID.
        /// </summary>
        [HttpGet("{id}")]

        public async Task<IActionResult> GetByAttendanceId(int id)
        {
            var attendance = await _attendanceServices.GetByAttendanceIdAsync(id);
            if (attendance == null) return NotFound($"Attendance with ID {id} not found");
            return Ok(attendance);
        }

        /// <summary>
        /// Registers a new attendance into the system.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAttendance(AttendanceDto.CreateAttendanceDto attendanceDto)
        {
            var success = await _attendanceServices.CreateAttendanceAsync(attendanceDto);
            if (!success) 
            {
                return BadRequest("Invalid StudentId or CourseId. Please ensure both exist in the database");
            }
            return Ok("Attendance Created Successfully");
        }

        /// <summary>
        /// Updates an existing attendance's information.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateAttendance(AttendanceDto.UpdateAttentanceDto attentanceDto)
        {
            var update = await _attendanceServices.UpdateAttendanceAsync(attentanceDto);
            if (!update) return NotFound("Update Failed: Attendance Not Found");
            return Ok("Attendance Updated Successfully");
        }

        /// <summary>
        /// Removes an attendance record from the system.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var delete = await _attendanceServices.DeleteAttendanceAsync(id);
            if (!delete) return NotFound("Delete Failed: Attendance Not Found");
            return Ok("Attendance Deleted Successfully");
        }
    }
}
