using Microsoft.AspNetCore.Mvc;
using IT_IJ_V2_AMS_DBTC.enums;

namespace IT_IJ_V2_AMS_DBTC.Models
{
    public class StudentModels : Controller
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public string Email { get; set; }

    }
}
