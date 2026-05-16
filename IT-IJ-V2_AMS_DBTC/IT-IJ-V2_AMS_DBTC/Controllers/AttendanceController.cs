using Microsoft.AspNetCore.Mvc;
using IT_IJ_V2_AMS_DBTC.Models;
using IT_IJ_V2_AMS_DBTC.enums;

namespace IT_IJ_V2_AMS_DBTC.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            List<AttendanceModels> attendanceList = new List<AttendanceModels>()
            {
                new AttendanceModels
                {
                    AttendanceId = 1,
                    StudentId = 101,
                    CourseId = 1,
                    Date = DateTime.Now,
                    Status = Status.Present
                },

                new AttendanceModels
                {
                    AttendanceId = 2,
                    StudentId = 102,
                    CourseId = 2,
                    Date = DateTime.Now,
                    Status = Status.Absent
                }
            };

            return View(attendanceList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}