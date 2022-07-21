using System;
using System.Collections.Generic;
using System.IO;


namespace HomeWork_Structures
{
    class Person
    {

        #region Поля
        /// <summary>
        /// Поле "Идентификатор"
        /// </summary>
        private int iD;

        /// <summary>
        /// Поле "Дата и время регистрации в базу"
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Поле "ФИО"
        /// </summary>
        private string name;

        /// <summary>
        /// Поле "Возраст"
        /// </summary>
        private int age;

        /// <summary>
        /// Поле "Рост"
        /// </summary>
        private int height;

        /// <summary>
        /// Поле "Дата рождения"
        /// </summary>
        private DateTime birthDay;

        /// <summary>
        /// Поле "Место рождения"
        /// </summary>
        private string placeOfBirth;
        #endregion

        #region Свойства
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get { return this.iD; } set { this.iD = value; } }

        /// <summary>
        /// Дата и время регистрации в базу
        /// </summary>
        public DateTime Date { get { return this.date; } set { this.date = value; } }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get { return this.age; } set { this.age = value; } }

        /// <summary>
        /// Рост
        /// </summary>
        public int Height { get { return this.height; } set { this.height = value; } }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDay { get { return this.birthDay; } set { this.birthDay = value; } }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string PlaceOfBirth { get { return this.placeOfBirth; } set { this.placeOfBirth = value; } }
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Name">Имя</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Height">Рост</param>
        /// <param name="BirthDay">День рождения</param>
        /// <param name="PlaceOfBirth">Место рождения</param>
        public Person(int ID, string Name, int Age, int Height, DateTime BirthDay, string PlaceOfBirth)
        {
            this.iD = ID;
            date = DateTime.Now;
            this.name = Name;
            this.age = Age;
            this.height = Height;
            this.birthDay = BirthDay;
            this.placeOfBirth = PlaceOfBirth;
        }

        /// <summary>
        /// Конструктор для записи информации о пользователе из файла
        /// </summary>
        /// <param name="ID">Идентификатор</param>
        /// <param name="Date">Дата и время регистрации в базу</param>
        /// <param name="Name">ФИО</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Height">Рост</param>
        /// <param name="BirthDay">Дата рождения</param>
        /// <param name="PlaceOfBirth">Место рождения</param>
        public Person(int ID, DateTime Date, string Name, int Age, int Height, DateTime BirthDay, string PlaceOfBirth)
        {
            this.ID = ID;
            this.Date = Date;
            this.Name = Name;
            this.Age = Age;
            this.Height = Height;
            this.BirthDay = BirthDay;
            this.PlaceOfBirth = PlaceOfBirth;
        }

        /// <summary>
        /// Конструктор для добавления нового пользователя
        /// </summary>
        /// <param name="ID">ID</param>
        public Person(int ID)
        {
            //Console.Write($"Введите ID: ");
            //bool suc = int.TryParse(Console.ReadLine(), out ID);
            this.ID = ID;
            //if (!suc) { ID = 0; }

            date = DateTime.Now;

            Console.Write($"Введите имя: ");
            name = Console.ReadLine();
            if (name == "") name = "Пассажир";

            Console.Write($"Введите возраст: ");
            bool suc1 = int.TryParse(Console.ReadLine(), out age);
            if (!suc1) { age = 0; }

            Console.Write($"Введите рост: ");
            bool suc2 = int.TryParse(Console.ReadLine(), out height);
            if (!suc2) { height = 0; }

            Console.Write($"Введите дату рождения: ");
            DateTime.TryParse(Console.ReadLine(), out birthDay);

            Console.Write($"Введите место рождения: ");
            placeOfBirth = Console.ReadLine();
            if (placeOfBirth == "") placeOfBirth = "с. Ебуньково";
        }
                
        public override string ToString()
        {
            //Console.WriteLine($"{iD}, {date}, {name}, {age}, {height}, {birthDay.ToShortDateString()}, {placeOfBirth}");
            return $"{ID}\t{Date}\t{Name}\t{Age}\t{Height}\t{BirthDay.ToShortDateString()}\t{PlaceOfBirth}";
        }


    }

}
