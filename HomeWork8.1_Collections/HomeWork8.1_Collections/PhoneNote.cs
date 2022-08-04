using System;
using System.Collections.Generic;
//using System.Linq;

namespace HomeWork8._1_Collections
{
    class PhoneNote
    {
        /// <summary>
        /// Коллекция для хранения телефонной книги
        /// </summary>
        private Dictionary<long, string> _phoneNumbers;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PhoneNote()
        {
            _phoneNumbers = new Dictionary<long, string>();
            _phoneNumbers.Add(79608551246, "Лысый");
            _phoneNumbers.Add(79512516682, "Еврей");
            _phoneNumbers.Add(79886662211, "Бешеный");
            _phoneNumbers.Add(79995552211, "Ныряльщик");
        }
        /// <summary>
        /// Метод добавления номера и проверка на наличие нового номера в справочнике
        /// </summary>
        private void AddPhoneNumberInNote()
        {
            while (true)
            {
                Console.Write($"Введите номер телефона (формат записи 7(8)ХХХХХХХХХХ): ");
                bool check = long.TryParse(Console.ReadLine().Replace(" ", "")
                    .Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", ""), out long key);
                if (check == false) { break; } ///проверка пустой строки

                Console.Write($"Введите имя: ");
                string value = Console.ReadLine(); if (value == "") { value = "Пёс"; };
                if (_phoneNumbers.ContainsKey(key) == false)
                {
                    _phoneNumbers.Add(key, value);
                }
                else
                {
                    Console.WriteLine($"Номер уже есть в справочнике");
                }
            }


        }
        /// <summary>
        /// Метод вывода списка на экран
        /// </summary>
        private void PrintPhoneNote()
        {
            foreach (KeyValuePair<long, string> pair in _phoneNumbers)
            {
                Console.WriteLine(pair);
            }
        }
        /// <summary>
        /// Метод поиска пользователя по номеру телефона в записной книжке
        /// </summary>
        private void SearchUserInNote()
        {
            Console.Write($"Введите номер телефона (без знака +): ");
            long.TryParse(Console.ReadLine().Replace(" ", "").Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", ""), out long key);

            if (_phoneNumbers.ContainsKey(key))
            {
                Console.WriteLine(_phoneNumbers[key]);
            }
            else
            {
                Console.WriteLine($"Где-то цифру проебал!");
            }
        }
        /// <summary>
        /// Возврат на главный экран меню
        /// </summary>
        public void ReturnToMenu(out char toMenu)
        {
            while (true)
            {
                Console.Write("Вернуться в меню? y/n "); ///запрос продолжения
                char.TryParse(Console.ReadLine(), out toMenu);
                if (toMenu == 'y' || toMenu == 'n')
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Метод вызова меню программы
        /// </summary>
        public void Menu()
        {
            char toMenu = 'y';
            while (char.ToLower(toMenu) == 'y')
            {
                Console.Clear();
                Console.Write($"1 - Добавление номера" +
                            $"\n2 - Показать список" +
                            $"\n3 - Поиск владельца" +
                            $"\nВыбор: ");
                byte.TryParse(Console.ReadLine(), out byte choise);
                switch (choise)
                {
                    /*Добавление номера*/
                    case 1:
                        Console.Clear();
                        AddPhoneNumberInNote();
                        ReturnToMenu(out toMenu);
                        continue;
                    /*Показать список*/
                    case 2:
                        Console.Clear();
                        PrintPhoneNote();
                        ReturnToMenu(out toMenu);
                        continue;
                    /*Поиск владельца*/
                    case 3:
                        Console.Clear();
                        SearchUserInNote();
                        ReturnToMenu(out toMenu);
                        continue;
                    default:
                        Console.Clear();
                        Console.WriteLine($"Ну Ёпта! Три цифры всего..");
                        ReturnToMenu(out toMenu);
                        continue;
                }
            }
        }
    }
}
