using IT_IJ_V2_AMS_DBTC.Models;

namespace IT_IJ_V2_AMS_DBTC.Services
{
    public class CourseService
    {
        // In-memory data store
        private static List<CourseModels> _courses = new List<CourseModels>
        {
            new CourseModels
            {
                CourseId    = 1,
                CourseCode  = "IT 210",
                Description = "Information Management"
            },
            new CourseModels
            {
                CourseId    = 2,
                CourseCode  = "IT 211",
                Description = "Web Application Development"
            },
            new CourseModels
            {
                CourseId    = 3,
                CourseCode  = "IT 212",
                Description = "Network Management 2"
            }
        };

        private static int _nextId = 4;

        // ── GET ALL ──────────────────────────────────────────
        public List<CourseModels> GetAll()
        {
            return _courses;
        }

        // ── GET BY ID ────────────────────────────────────────
        public CourseModels? Get(int id)
        {
            return _courses.FirstOrDefault(c => c.CourseId == id);
        }

        // ── CREATE ───────────────────────────────────────────
        public CourseModels Create(CourseModels course)
        {
            course.CourseId = _nextId++;
            _courses.Add(course);
            return course;
        }

        // ── EDIT ─────────────────────────────────────────────
        public CourseModels? Edit(CourseModels course)
        {
            var existing = _courses.FirstOrDefault(c => c.CourseId == course.CourseId);
            if (existing != null)
            {
                existing.CourseCode = course.CourseCode;
                existing.Description = course.Description;
            }
            return existing;
        }

        // ── DELETE ───────────────────────────────────────────
        public bool Delete(int id)
        {
            var course = _courses.FirstOrDefault(c => c.CourseId == id);
            if (course != null)
            {
                _courses.Remove(course);
                return true;
            }
            return false;
        }
    }
}