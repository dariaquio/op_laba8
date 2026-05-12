using System;

namespace ElectronicDeanery.Domain
{
    // базовий клас "Особа"
    // демонструє відношення УЗАГАЛЬНЕННЯ — від нього наслідується Student
    public class Person
    {
        // прізвище
        public string LastName { get; set; }

        // ім'я
        public string FirstName { get; set; }

        // дата народження
        public DateTime BirthDate { get; set; }

        // конструктор з валідацією
        public Person(string lastName, string firstName, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Прізвище не може бути порожнім");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Ім'я не може бути порожнім");

            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
        }

        // повертає повне ім'я особи (для перевизначення нащадками)
        public virtual string GetInfo()
        {
            return $"{LastName} {FirstName} ({BirthDate:dd.MM.yyyy})";
        }
    }
}
