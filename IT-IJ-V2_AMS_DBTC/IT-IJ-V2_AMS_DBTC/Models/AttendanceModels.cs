using Microsoft.AspNetCore.Mvc;
using IT_IJ_V2_AMS_DBTC.enums;

namespace IT_IJ_V2_AMS_DBTC.Models
{
    public class AttendanceModels
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }

    }
}
