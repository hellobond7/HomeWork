using System;
using System.Collections.Generic;

namespace HomeWork8._1_Collections
{
    class RepeatNumber
    {
        /// <summary>
        /// Коллекция для хранения чисел
        /// </summary>
        private HashSet<int> _repeatNumbers;

        public RepeatNumber()
        {
            _repeatNumbers = new HashSet<int>();
            _repeatNumbers.Add(1);
            _repeatNumbers.Add(2);
            _repeatNumbers.Add(3);
            _repeatNumbers.Add(4);
        }
        /// <summary>
        /// Метод для ввода числа и проверка его уникальности
        /// </summary>
        public void AddNumberToHashCollection()
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"Список чисел: ");
                foreach (var item in _repeatNumbers) { Console.Write($"{item} "); }

                Console.Write($"\nВведите число: ");
                int.TryParse(Console.ReadLine(), out int number);
                if (_repeatNumbers.Contains(number) == false)
                {
                    _repeatNumbers.Add(number);
                    Console.WriteLine($"Число успешно добавлено!");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Введеное число уже есть в списке!");
                    Console.ReadKey();
                }
            }
        }

    }
}
