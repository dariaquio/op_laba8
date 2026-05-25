using System;
using ElectronicDeanery.Domain;

namespace ElectronicDeanery.UI
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Deanery deanery = new Deanery();

            // створення груп
            Group groupIP55 = new Group("ІП-55", "Інженерія програмного забезпечення(ІПІ)");
            Group groupIM55 = new Group("ІМ-55", "Інженерія програмного забезпечення(ОТ)");
            Group groupIK51 = new Group("ІК-51", "Компʼютерна інженерія");
            deanery.AddGroup(groupIP55);
            deanery.AddGroup(groupIM55);
            deanery.AddGroup(groupIK51);

            // створення студентів
            Student s1 = new Student("IP001", "Костюк", "Дарʼя", new DateTime(2007, 8, 30));
            Student s2 = new Student("IP002", "Сушко", "Марія", new DateTime(2007, 10, 25));
            Student s3 = new Student("IP003", "Шапкіна", "Іванна", new DateTime(2008, 1, 31));
            Student s4 = new Student("IM001", "Іваненко", "Іван", new DateTime(2008, 6, 12));
            Student s5 = new Student("IM002", "Пупкін", "Василь", new DateTime(2007, 9, 1));
            Student s6 = new Student("IK001", "Овчар", "Олександр", new DateTime(2007, 11, 15));

            deanery.AddStudent(s1);
            deanery.AddStudent(s2);
            deanery.AddStudent(s3);
            deanery.AddStudent(s4);
            deanery.AddStudent(s5);
            deanery.AddStudent(s6);

            // додавання студентів у групи (асоціація)
            groupIP55.AddStudent(s1);
            groupIP55.AddStudent(s2);
            groupIP55.AddStudent(s3);
            groupIM55.AddStudent(s4);
            groupIM55.AddStudent(s5);
            groupIK51.AddStudent(s6);
            // створення гуртожитку та кімнат (композиція)
            Dormitory dorm = new Dormitory(20, "вул. Борщагівська, 144");
            dorm.AddRoom(101, 2);
            dorm.AddRoom(102, 3);
            dorm.AddRoom(103, 2);
            deanery.AddDormitory(dorm);

            // поселення
            dorm.AccommodateStudent(101, s1);
            dorm.AccommodateStudent(103, s4);
            dorm.AccommodateStudent(103, s5);

            // виведення списку всіх студентів
            Console.WriteLine("Усі студенти");
            foreach (Student s in deanery.Students)
                Console.WriteLine("  " + s.GetInfo());

            // виведення груп з їх студентами
            Console.WriteLine("\nГрупи");
            foreach (Group g in deanery.Groups)
            {
                Console.WriteLine($"  {g.Name} ({g.Specialty}) — {g.Students.Count} студ.");
                foreach (Student st in g.Students)
                    Console.WriteLine($"     {st.LastName} {st.FirstName}");
            }

            // інфо про гуртожиток
            Console.WriteLine($"\nГуртожиток №{dorm.Number} ({dorm.Address})");
            Console.WriteLine($"  Усього мешканців: {dorm.TotalResidents}, вільних місць: {dorm.TotalFreeSpaces}");
            foreach (Room r in dorm.Rooms)
            {
                Console.WriteLine($"  Кімната {r.Number} (місць: {r.Capacity}, вільно: {r.FreeSpaces})");
                foreach (Student st in r.Residents)
                    Console.WriteLine($"     {st.LastName} {st.FirstName}");
            }

            // пошук (залежність)
            SearchService search = new SearchService();

            Console.WriteLine("\nПошук за ключовим словом 'остюк'");
            foreach (Student s in search.SearchByKeyword(deanery.Students, "остюк"))
                Console.WriteLine("  " + s.GetInfo());

            Console.WriteLine("\nПошук за групою ІП-55");
            foreach (Student s in search.SearchByGroup(groupIP55))
                Console.WriteLine("  " + s.GetInfo());

            Console.WriteLine("\n Пошук студентів, що проживають у гуртожитку №20");
            foreach (Student s in search.SearchInDormitory(dorm))
                Console.WriteLine("  " + s.GetInfo());

            // зміна даних
            Console.WriteLine("\nРедагування даних");
            s2.FirstName = "Марина";
            Console.WriteLine("  Змінено ім'я студента IP002: " + s2.GetInfo());

            // виписка студента з гуртожитку
            dorm.EvictStudent(s1);
            Console.WriteLine($"  Студента {s1.LastName} виписано з гуртожитку");

            // видалення студента з групи (агрегація — об'єкт студента залишається)
            groupIM55.RemoveStudent(s5);
            Console.WriteLine($"  Студента {s5.LastName} видалено з групи IM-55");
            Console.WriteLine($"  Студент досі існує: {s5.GetInfo()}");

            // видалення групи (студенти залишаються — агрегація)
            Console.WriteLine($"\nВидалення групи ІК-51");
            deanery.RemoveGroup(groupIK51);
            Console.WriteLine("  Залишилося груп: " + deanery.Groups.Count);
            Console.WriteLine($"  Студент {s6.LastName} досі існує: {s6.GetInfo()}");

            // обробкa виключень
            Console.WriteLine("\nОбробка виключень:");
            try
            {
                dorm.AccommodateStudent(103, s3); // кімната вже повна
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("  Помилка! " + ex.Message);
            }

            try
            {
                Student bad = new Student("", "Тест", "Тест", DateTime.Now); // порожня заліковка
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  Помилка! " + ex.Message);
            }

            try
            {
                dorm.AddRoom(101, 2); // кімната 101 вже існує
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("  Помилка! " + ex.Message);
            }
        }
    }
}
