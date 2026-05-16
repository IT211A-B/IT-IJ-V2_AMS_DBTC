using IT_IJ_V2_AMS_DBTC.Models;
using IT_IJ_V2_AMS_DBTC.enums;

namespace IT_IJ_V2_AMS_DBTC.Services
{
    public class StudentService
    {
        // In-memory data store
        private static List<StudentModels> _students = new List<StudentModels>
        {
            new StudentModels
            {
                StudentId = 1,
                FirstName = "Juan",
                LastName  = "Dela Cruz",
                Sex       = Sex.Male,
                Email     = "juan.delacruz@email.com"
            },
            new StudentModels
            {
                StudentId = 2,
                FirstName = "Maria",
                LastName  = "Santos",
                Sex       = Sex.Female,
                Email     = "maria.santos@email.com"
            }
        };

        private static int _nextId = 3;

        // ── GET ALL ──────────────────────────────────────────
        public List<StudentModels> GetAll()
        {
            return _students;
        }

        // ── GET BY ID ────────────────────────────────────────
        public StudentModels? Get(int id)
        {
            return _students.FirstOrDefault(s => s.StudentId == id);
        }

        // ── CREATE ───────────────────────────────────────────
        public StudentModels Create(StudentModels student)
        {
            student.StudentId = _nextId++;
            _students.Add(student);
            return student;
        }

        // ── EDIT ─────────────────────────────────────────────
        public StudentModels? Edit(StudentModels student)
        {
            var existing = _students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if (existing != null)
            {
                existing.FirstName = student.FirstName;
                existing.LastName = student.LastName;
                existing.Sex = student.Sex;
                existing.Email = student.Email;
            }
            return existing;
        }

        // ── DELETE ───────────────────────────────────────────
        public bool Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.StudentId == id);
            if (student != null)
            {
                _students.Remove(student);
                return true;
            }
            return false;
        }
    }
}
