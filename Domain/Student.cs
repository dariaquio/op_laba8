using System;

namespace ElectronicDeanery.Domain
{
    /// Клас "Студент".
    /// УЗАГАЛЬНЕННЯ: наслідується від Person.
    /// РЕАЛІЗАЦІЯ: реалізує інтерфейс ISearchable.
    /// АСОЦІАЦІЯ: має посилання на Group (студент "навчається у" групі)
    public class Student : Person, ISearchable
    {
        /// Номер залікової книжки
        public string StudentId { get; private set; }

        /// Група, в якій навчається студент (асоціація).
        public Group StudentGroup { get; set; }

        /// Конструктор студента
        public Student(string studentId, string lastName, string firstName, DateTime birthDate)
            : base(lastName, firstName, birthDate)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Номер залікової книжки не може бути порожнім");

            StudentId = studentId;
        }

        ///Поліморфізм: перевизначення методу базового класу
        public override string GetInfo()
        {
            string groupName = StudentGroup != null ? StudentGroup.Name : "без групи";
            return $"№{StudentId} {LastName} {FirstName} [{groupName}]";
        }

        /// Реалізація методу інтерфейсу ISearchable — пошук за прізвищем чи ім'ям
        public bool Matches(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return false;
            keyword = keyword.ToLower();
            return LastName.ToLower().Contains(keyword)
                || FirstName.ToLower().Contains(keyword);
        }
    }
}
