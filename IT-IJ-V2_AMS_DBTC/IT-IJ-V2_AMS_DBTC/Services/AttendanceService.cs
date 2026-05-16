using IT_IJ_V2_AMS_DBTC.enums;
using IT_IJ_V2_AMS_DBTC.Models;

using IT_IJ_V2_AMS_DBTC.Models;
using IT_IJ_V2_AMS_DBTC.enums;
 
namespace IT_IJ_V2_AMS_DBTC.Services
{
    public class AttendanceService
    {
      
        private static List<AttendanceModels> _attendances = new List<AttendanceModels>
        {
            new AttendanceModels
            {
                AttendanceId = 1,
                StudentId    = 101,
                CourseId     = 1,
                Date         = DateTime.Now,
                Status       = Status.Present
            },
            new AttendanceModels
            {
                AttendanceId = 2,
                StudentId    = 102,
                CourseId     = 2,
                Date         = DateTime.Now,
                Status       = Status.Absent
            }
        };

        private static int _nextId = 3;

       
        public List<AttendanceModels> GetAll()
        {
            return _attendances;
        }

        // ── GET BY ID ────────────────────────────────────────
        public AttendanceModels? Get(int id)
        {
            return _attendances.FirstOrDefault(a => a.AttendanceId == id);
        }

        // ── CREATE ───────────────────────────────────────────
        public AttendanceModels Create(AttendanceModels attendance)
        {
            attendance.AttendanceId = _nextId++;
            _attendances.Add(attendance);
            return attendance;
        }

        // ── EDIT ─────────────────────────────────────────────
        public AttendanceModels? Edit(AttendanceModels attendance)
        {
            var existing = _attendances.FirstOrDefault(a => a.AttendanceId == attendance.AttendanceId);
            if (existing != null)
            {
                existing.StudentId = attendance.StudentId;
                existing.CourseId = attendance.CourseId;
                existing.Date = attendance.Date;
                existing.Status = attendance.Status;
            }
            return existing;
        }

        // ── DELETE ───────────────────────────────────────────
        public bool Delete(int id)
        {
            var attendance = _attendances.FirstOrDefault(a => a.AttendanceId == id);
            if (attendance != null)
            {
                _attendances.Remove(attendance);
                return true;
            }
            return false;
        }
    }
}