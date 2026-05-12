using System.Collections.Generic;

namespace ElectronicDeanery.Domain
{
    // клас "Деканат" — головний клас системи, який зберігає всіх студентів групи гуртожитки
    public class Deanery
    {
        // усі студенти закладу
        public List<Student> Students { get; private set; }

        // усі групи
        public List<Group> Groups { get; private set; }

        // усі гуртожитки
        public List<Dormitory> Dormitories { get; private set; }

        // конструктор
        public Deanery()
        {
            Students = new List<Student>();
            Groups = new List<Group>();
            Dormitories = new List<Dormitory>();
        }

        // додати студента
        public void AddStudent(Student student) => Students.Add(student);

        // видалити студента (видаляє з групи та виписує з гуртожитку)
        public void RemoveStudent(Student student)
        {
            if (student.StudentGroup != null)
                student.StudentGroup.RemoveStudent(student);

            foreach (Dormitory d in Dormitories)
            {
                Room room = d.FindRoomByStudent(student);
                if (room != null)
                    room.RemoveResident(student);
            }

            Students.Remove(student);
        }

        // додати групу
        public void AddGroup(Group group) => Groups.Add(group);

        // видалити групу (студенти при цьому залишаються — агрегація)
        public void RemoveGroup(Group group)
        {
            foreach (Student s in new List<Student>(group.Students))
                group.RemoveStudent(s);

            Groups.Remove(group);
        }

        // додати гуртожиток
        public void AddDormitory(Dormitory dormitory) => Dormitories.Add(dormitory);
    }
}
