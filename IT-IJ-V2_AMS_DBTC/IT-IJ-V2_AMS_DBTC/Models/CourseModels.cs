using Microsoft.AspNetCore.Mvc;

namespace IT_IJ_V2_AMS_DBTC.Models
{
    public class CourseModels : Controller
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
    }
}
