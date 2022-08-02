using System.Collections.Generic;

namespace HomeWork_Structures
{
    class Sort : IComparer<Person>
    {
        /// <summary>
        /// Переменная выбора действия
        /// </summary>
        private byte _choise;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="choise">Переменная выбора действия</param>
        public Sort(byte choise)
        {
            this._choise = choise;
        }

        /// <summary>
        /// Сортировщик по выбранным полям
        /// </summary>
        /// <param name="p1">Первый параметр для сравнения</param>
        /// <param name="p2">Второй параметр для сравнения</param>
        /// <returns></returns>
        public int Compare(Person p1, Person p2)
        {
            switch (_choise)
            {
                case 1: ///Сортировка по ID
                    if (p1.ID < p2.ID) return -1;
                    if (p1.ID == p2.ID) return 0;
                    return 1;

                case 2: ///Сортировка по дате
                    if (p1.Date < p2.Date) return -1;
                    if (p1.Date == p2.Date) return 0;
                    return 1;

                case 3: ///Сортировка по дате
                    int i = p1.Name.CompareTo(p2.Name);
                    return i;

                case 4: ///Сортировка по возрасту
                    if (p1.Age < p2.Age) return -1;
                    if (p1.Age == p2.Age) return 0;
                    return 1;

                case 5: ///Сортировка по росту
                    if (p1.Height < p2.Height) return -1;
                    if (p1.Height == p2.Height) return 0;
                    return 1;

                case 6: ///Сортировка по дате рождения
                    if (p1.BirthDay < p2.BirthDay) return -1;
                    if (p1.BirthDay == p2.BirthDay) return 0;
                    return 1;

                case 7: ///Сортировка по месту рождения
                    int j = p1.PlaceOfBirth.CompareTo(p2.PlaceOfBirth);
                    return j;

                default:
                    System.Console.WriteLine("Не выбран ни один из вариантов");
                    return 0;
            }
        }
    }
}
