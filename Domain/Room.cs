using System;
using System.Collections.Generic;

namespace ElectronicDeanery.Domain
{
    /// клас "Кімната" в гуртожитку
    /// існує лише в межах гуртожитку (демонструє композицію з боку Dormitory)
    public class Room
    {
        /// номер кімнати
        public int Number { get; private set; }

        /// максимальна кількість мешканців
        public int Capacity { get; private set; }

        /// список поселених студентів у кімнаті
        public List<Student> Residents { get; private set; }

        /// конструктор кімнати
        public Room(int number, int capacity)
        {
            if (number <= 0)
                throw new ArgumentException("Номер кімнати має бути додатнім");
            if (capacity <= 0)
                throw new ArgumentException("Місткість має бути додатньою");

            Number = number;
            Capacity = capacity;
            Residents = new List<Student>();
        }

        /// кількість вільних місць
        public int FreeSpaces => Capacity - Residents.Count;

        /// поселити студента в кімнату
        public void AddResident(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (FreeSpaces <= 0)
                throw new InvalidOperationException($"У кімнаті №{Number} немає вільних місць");
            if (Residents.Contains(student))
                throw new InvalidOperationException("Студент вже поселений у цій кімнаті");

            Residents.Add(student);
        }

        /// виписати студента з кімнати
        public void RemoveResident(Student student)
        {
            if (!Residents.Remove(student))
                throw new InvalidOperationException("Студент не знайдений у цій кімнаті");
        }
    }
}
