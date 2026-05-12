using System.Collections.Generic;
using System.Linq;

namespace ElectronicDeanery.Domain
{
    /// пошук студентів
    /// (залежність) використовує об'єкти Student, Group, Dormitory як параметри методів
    public class SearchService
    {
        /// пошук студентів за ключовим словом (прізвище або ім'я)
        public List<Student> SearchByKeyword(List<Student> students, string keyword)
        {
            return students.Where(s => s.Matches(keyword)).ToList();
        }

        /// пошук студентів певної групи
        public List<Student> SearchByGroup(Group group)
        {
            return new List<Student>(group.Students);
        }

        /// пошук студентів, що проживають у вказаному гуртожитку.
        public List<Student> SearchInDormitory(Dormitory dormitory)
        {
            return dormitory.Rooms.SelectMany(r => r.Residents).ToList();
        }
    }
}
