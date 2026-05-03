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

        [HttpGet]
        public async Task<IActionResult> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceServices.GetAllAttendancesAsync();
            return Ok(attendances);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByAttendanceId(int id)
        {
            var attendance = await _attendanceServices.GetByAttendanceIdAsync(id);
            if (attendance == null) return NotFound();
            return Ok(attendance);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendance(AttendanceDto.CreateAttendanceDto attendanceDto)
        {
            await _attendanceServices.CreateAttendanceAsync(attendanceDto);
            return Ok("Attendance Created Successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAttendance(AttendanceDto.UpdateAttentanceDto attentanceDto)
        {
            await _attendanceServices.UpdateAttendanceAsync(attentanceDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            await _attendanceServices.DeleteAttendanceAsync(id);
            return Ok("Attendance Deleted Successfully");
        }
    }
}
