using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicDeanery.Domain
{
    // клас "Гуртожиток"
    // (композиція) містить кімнати (Room) кімнати створюються та існують лише в межах гуртожитку,
    // при видаленні гуртожитку всі кімнати знищуються разом з ним.
    public class Dormitory
    {
        // номер гуртожитку
        public int Number { get; private set; }

        // адреса гуртожитку
        public string Address { get; set; }

        // кімнати гуртожитку (композиція)
        public List<Room> Rooms { get; private set; }

        // конструктор гуртожитку
        public Dormitory(int number, string address)
        {
            if (number <= 0)
                throw new ArgumentException("Номер гуртожитку має бути додатнім");

            Number = number;
            Address = address;
            Rooms = new List<Room>();
        }

        // додати кімнату в гуртожиток (створюється новий об'єкт Room — композиція)
        public Room AddRoom(int roomNumber, int capacity)
        {
            if (Rooms.Any(r => r.Number == roomNumber))
                throw new InvalidOperationException($"Кімната №{roomNumber} вже існує");

            Room room = new Room(roomNumber, capacity);
            Rooms.Add(room);
            return room;
        }

        // поселити студента в зазначену кімнату
        public void AccommodateStudent(int roomNumber, Student student)
        {
            Room room = Rooms.FirstOrDefault(r => r.Number == roomNumber);
            if (room == null)
                throw new InvalidOperationException($"Кімната №{roomNumber} не існує");

            room.AddResident(student);
        }

        // виписати студента з гуртожитку
        public void EvictStudent(Student student)
        {
            Room room = Rooms.FirstOrDefault(r => r.Residents.Contains(student));
            if (room == null)
                throw new InvalidOperationException("Студент не проживає в цьому гуртожитку");

            room.RemoveResident(student);
        }

        // загальна кількість мешканців
        public int TotalResidents => Rooms.Sum(r => r.Residents.Count);

        // загальна кількість вільних місць
        public int TotalFreeSpaces => Rooms.Sum(r => r.FreeSpaces);

        // пошук кімнати, де проживає студент
        public Room FindRoomByStudent(Student student)
        {
            return Rooms.FirstOrDefault(r => r.Residents.Contains(student));
        }
    }
}
