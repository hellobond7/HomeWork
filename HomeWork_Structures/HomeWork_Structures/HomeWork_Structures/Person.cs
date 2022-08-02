using System;


namespace HomeWork_Structures
{
    class Person
    {
        #region Поля
        /// <summary>
        /// Поле "Идентификатор"
        /// </summary>
        private int _iD;

        /// <summary>
        /// Поле "Дата и время регистрации в базу"
        /// </summary>
        private DateTime _date;

        /// <summary>
        /// Поле "ФИО"
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле "Возраст"
        /// </summary>
        private int _age;

        /// <summary>
        /// Поле "Рост"
        /// </summary>
        private int _height;

        /// <summary>
        /// Поле "Дата рождения"
        /// </summary>
        private DateTime _birthDay;

        /// <summary>
        /// Поле "Место рождения"
        /// </summary>
        private string _placeOfBirth;
        #endregion

        #region Свойства
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get { return this._iD; } set { this._iD = value; } }

        /// <summary>
        /// Дата и время регистрации в базу
        /// </summary>
        public DateTime Date { get { return this._date; } set { this._date = value; } }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get { return this._name; } set { this._name = value; } }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get { return this._age; } set { this._age = value; } }

        /// <summary>
        /// Рост
        /// </summary>
        public int Height { get { return this._height; } set { this._height = value; } }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDay { get { return this._birthDay; } set { this._birthDay = value; } }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string PlaceOfBirth { get { return this._placeOfBirth; } set { this._placeOfBirth = value; } }
        #endregion

        #region Конструктор
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
            this._iD = ID;
            _date = DateTime.Now;
            this._name = Name;
            this._age = Age;
            this._height = Height;
            this._birthDay = BirthDay;
            this._placeOfBirth = PlaceOfBirth;
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

            _date = DateTime.Now;

            Console.Write($"Введите имя: ");
            _name = Console.ReadLine();
            if (_name == "") _name = "Пассажир";

            Console.Write($"Введите возраст: ");
            bool suc1 = int.TryParse(Console.ReadLine(), out _age);
            if (!suc1) { _age = 0; }

            Console.Write($"Введите рост: ");
            bool suc2 = int.TryParse(Console.ReadLine(), out _height);
            if (!suc2) { _height = 0; }

            Console.Write($"Введите дату рождения: ");
            DateTime.TryParse(Console.ReadLine(), out _birthDay);

            Console.Write($"Введите место рождения: ");
            _placeOfBirth = Console.ReadLine();
            if (_placeOfBirth == "") _placeOfBirth = "с. Ебуньково";
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод печати полей с информацией о пользователе
        /// </summary>
        /// <returns>Строка с заполненными данными</returns>
        public override string ToString()
        {
            //Console.WriteLine($"{iD}, {date}, {name}, {age}, {height}, {birthDay.ToShortDateString()}, {placeOfBirth}");
            return $"{ID}\t{Date}\t{Name}\t{Age}\t{Height}\t{BirthDay.ToShortDateString()}\t{PlaceOfBirth}";
        }
        #endregion
    }

}
