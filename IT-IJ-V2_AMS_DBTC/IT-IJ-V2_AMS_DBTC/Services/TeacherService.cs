using IT_IJ_V2_AMS_DBTC.Models;
using IT_IJ_V2_AMS_DBTC.enums;

namespace IT_IJ_V2_AMS_DBTC.Services
{
    public class TeacherService
    {
        // In-memory data store
        private static List<TeacherModels> _teachers = new List<TeacherModels>
        {
            new TeacherModels
            {
                TeacherId = 1,
                FirstName = "Mark",
                LastName  = "Atadero",
                Sex       = Sex.Male,
                Email     = "mark.atadero@email.com"
            },
            new TeacherModels
            {
                TeacherId = 2,
                FirstName = "Amor",
                LastName  = "Tolentino",
                Sex       = Sex.Female,
                Email     = "amor.tolentino@email.com"
            }
        };

        private static int _nextId = 3;

        // ── GET ALL ──────────────────────────────────────────
        public List<TeacherModels> GetAll()
        {
            return _teachers;
        }

        // ── GET BY ID ────────────────────────────────────────
        public TeacherModels? Get(int id)
        {
            return _teachers.FirstOrDefault(t => t.TeacherId == id);
        }

        // ── CREATE ───────────────────────────────────────────
        public TeacherModels Create(TeacherModels teacher)
        {
            teacher.TeacherId = _nextId++;
            _teachers.Add(teacher);
            return teacher;
        }

        // ── EDIT ─────────────────────────────────────────────
        public TeacherModels? Edit(TeacherModels teacher)
        {
            var existing = _teachers.FirstOrDefault(t => t.TeacherId == teacher.TeacherId);
            if (existing != null)
            {
                existing.FirstName = teacher.FirstName;
                existing.LastName = teacher.LastName;
                existing.Sex = teacher.Sex;
                existing.Email = teacher.Email;
            }
            return existing;
        }

        // ── DELETE ───────────────────────────────────────────
        public bool Delete(int id)
        {
            var teacher = _teachers.FirstOrDefault(t => t.TeacherId == id);
            if (teacher != null)
            {
                _teachers.Remove(teacher);
                return true;
            }
            return false;
        }
    }
}