using System;
using System.Collections.Generic;

namespace ElectronicDeanery.Domain
{
    // клас "Група"
    // (агрегація) містить студентів, але студенти можуть існувати і поза групою.
    // при видаленні групи об'єкти студентів продовжують існувати
    public class Group
    {
        // назва групи
        public string Name { get; set; }

        // спеціальність
        public string Specialty { get; set; }

        // список студентів групи (агрегація)
        public List<Student> Students { get; private set; }

        // конструктор групи
        public Group(string name, string specialty)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва групи не може бути порожньою");

            Name = name;
            Specialty = specialty;
            Students = new List<Student>();
        }

        // додати студента в групу
        public void AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (Students.Contains(student))
                throw new InvalidOperationException("Студент вже в групі");

            Students.Add(student);
            student.StudentGroup = this;
        }

        // видалити студента з групи (студент при цьому не знищується — це агрегація)
        public void RemoveStudent(Student student)
        {
            if (!Students.Remove(student))
                throw new InvalidOperationException("Студент не знайдений у групі");

            if (student.StudentGroup == this)
                student.StudentGroup = null;
        }
    }
}
