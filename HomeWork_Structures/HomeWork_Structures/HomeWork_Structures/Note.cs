using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HomeWork_Structures
{
    #region Заметки
    ///Разобраться с количеством пользователей и ID
    ///Разобраться с выводом списка на экран
    #endregion

    internal class Note
    {
        /// <summary>
        /// Коллекция для хранения пользователей
        /// </summary>
        private List<Person> _personList;

        /// <summary>
        /// Переменная для хранения информации о пользователе
        /// </summary>
        private Person _person;

        /// <summary>
        /// Путь к файлу с данными о пользователях
        /// </summary>
        private FileInfo _fileAdressForNotebook;

        /// <summary>
        /// Путь к файлу с данными о количестве пользователей
        /// </summary>
        private FileInfo _fileAdressForCountOfPerson;

        /// <summary>
        /// Последний занятый номер ID в записной книжке
        /// </summary>
        private int _ID;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Note(FileInfo fileAdress, FileInfo fileAdressForCountOfPerson)
        {
            _fileAdressForNotebook = fileAdress;
            _fileAdressForCountOfPerson = fileAdressForCountOfPerson;
            CheckFile(); ///вызов метода проверки существования файла
            _ID = CountOfPerson();
            _personList = new List<Person>();
        }

        /// <summary>
        /// Метод добавления нового пользователя
        /// </summary>
        public void AddPerson()
        {
            _ID++; ///увеличение счетчика
            _person = new Person(_ID); /// инициализация переменной типа Person
            LoadToFile(_person); ///вызов метода записи нового пользователя
        }

        /// <summary>
        /// Метод записи информации о новом пользователе в текстовый файл
        /// </summary>
        /// <param name="newPerson">Новый пользователь</param>
        public void LoadToFile(Person newPerson)
        {
            using (StreamWriter strWriterNotebook = new StreamWriter(_fileAdressForNotebook.ToString(), true, Encoding.Unicode)) ///объявление потока записи
                strWriterNotebook.WriteLine(newPerson.ToString()); ///запись информации о пользователе в файл

            using (StreamWriter strWriterCountOfPerson = new StreamWriter(_fileAdressForCountOfPerson.ToString(), false, Encoding.Unicode)) ///объявление потока записи
                strWriterCountOfPerson.WriteLine(_ID); ///запись информации о пользователе в файл
}

        /// <summary>
        /// Метод перезаписи информации о пользователях в текстовый файл
        /// </summary>
        public void ReloadToFile()
        {
            using (StreamWriter strWriterForNotebook = new StreamWriter(_fileAdressForNotebook.ToString(), false, Encoding.Unicode)) ///объявление потока записи
                foreach (var dir in _personList)
                {
                    strWriterForNotebook.WriteLine(dir.ToString()); ///запись строки в файл
                }
            using (StreamWriter strWriterCountOfPerson = new StreamWriter(_fileAdressForCountOfPerson.ToString(), false, Encoding.Unicode)) ///объявление потока записи
                strWriterCountOfPerson.WriteLine(_ID); ///запись информации о пользователе в файл
        }

        /// <summary>
        /// Метод проверки существования файла. Если файла не существует - то он создается
        /// </summary>
        public void CheckFile()
        {
            FileInfo fileInfNotebook = new FileInfo(_fileAdressForNotebook.ToString());
            if (fileInfNotebook.Exists == false)
            {
                fileInfNotebook.Create().Close(); ///создание пустого файла
            }
            FileInfo fileInfCountOfPerson = new FileInfo(_fileAdressForCountOfPerson.ToString());
            if (fileInfCountOfPerson.Exists == false)
            {
                fileInfCountOfPerson.Create().Close(); ///создание пустого файла
            }
        }

        /// <summary>
        /// Метод чтения записей из файла и запись в коллекцию для дальнейшего взаимодействия
        /// </summary>
        public void ReadFileForEditing()
        {
            char[] delimiter = new char[] { '\t' }; /// инициализация массива разделителей
            _personList.Clear();
            using (StreamReader strReader = new StreamReader(_fileAdressForNotebook.ToString())) ///объявление потока чтения
                while (!strReader.EndOfStream)
                {
                    string inputString = strReader.ReadLine(); ///инициализация переменной для хранения строки с информацией о пользователе
                    string[] inputEntry = inputString.Split(delimiter); ///перенос содержания записи в массив

                    int.TryParse(inputEntry[0], out int tempID);
                    DateTime.TryParse(inputEntry[1], out DateTime tempDate);
                    string tempName = inputEntry[2];
                    int.TryParse(inputEntry[3], out int tempAge);
                    int.TryParse(inputEntry[4], out int tempHeight);
                    DateTime.TryParse(inputEntry[5], out DateTime tempBirthDay);
                    string tempPlaceOfBirth = inputEntry[6];

                    Person person = new Person(tempID, tempDate, tempName, tempAge, tempHeight, tempBirthDay, tempPlaceOfBirth);
                    _personList.Add(person);
                }
        }

        /// <summary>
        /// Метод чтения информации о пользователе из файла и вывод на экран
        /// </summary>
        public void ReadFileAndPrintToConsole()
        {
            CheckFile();
            int count = 0; ///инициализация переменной счетчика количества записей в записной книжке
            using (StreamReader strReader = new StreamReader(_fileAdressForNotebook.ToString())) ///объявление потока чтения
                while (!strReader.EndOfStream)
                {
                    Console.WriteLine(strReader.ReadLine()); ///чтение файла построчно и вывод на экран
                    count++; ///увеличение счетчика
                }
            if (count == 0)
            {
                Console.WriteLine("В записной книжке нет ни одной записи!"); ///вывод сообщения, если файл пустой
            }
        }

        /// <summary>
        /// Метод чтения информации о количестве записей в файле
        /// </summary>
        /// <returns>Число записей в записной книжке</returns>
        public int CountOfPerson()
        {
            int count = 0;///инициализация переменной счетчика количества записей в записной книжке
            using (StreamReader strReader = new StreamReader(_fileAdressForCountOfPerson.ToString())) ///объявление потока чтения
                while (!strReader.EndOfStream)
                {
                    int.TryParse(strReader.ReadLine(), out count);
                }
            return count;
        }

        /// <summary>
        /// Удаления информации о пользователе по его ID
        /// </summary>
        /// <param name="ID">ID пользователя информацию о котором необходимо удалить</param>
        public void RemoveEntryFromFile(int ID)
        {
            ReadFileForEditing(); ///вывод на экран полного списка пользователей
            int count = 0; ///инициализация счетчика

            foreach (var item in _personList)
            {
                if (item.ID == ID)
                {
                    _personList.RemoveAt(count); ///удаление записи из коллекции
                    Console.WriteLine($"Информация о пользователе:\n{item}\nУспешно удалена!\n"); ///вывод удаляемой
                                                                                                  ///и сообщения об успешном выполнении программы
                    break;
                }
                count++;
            }
            ReloadToFile(); ///перезапись нового списка в файл
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
        /// Повтор действия
        /// </summary>
        /// <param name="toContinue"></param>
        public void RepeatAction(out char toContinue)
        {
            while (true)
            {
                Console.Write($"Повторить действие? y/n "); ///запрос повторения
                char.TryParse(Console.ReadLine(), out toContinue);
                if (toContinue == 'y' || toContinue == 'n')
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Сортировка списка по выбранному диапазону дат
        /// </summary>
        /// <param name="rangeStart">Начало диапазона</param>
        /// <param name="endOfRange">Конец диапазона</param>
        public void SortNote(DateTime rangeStart, DateTime endOfRange)
        {
            foreach (var item in _personList)
            {
                if (item.BirthDay > rangeStart && item.BirthDay < endOfRange) ///условие проверки соответствия заданным условиям
                {
                    Console.WriteLine(item); ///вывод строки в консоль
                }
            }
        }

        /// <summary>
        /// Метод изменения информации о пользователе
        /// </summary>
        /// <param name="ID">ID пользователя информацию о котором необходимо изменить</param>
        public void UserEditing(int ID, byte ChoiseForEditing)
        {
            foreach (var item in _personList)
            {
                if (item.ID == ID)
                {
                    switch (ChoiseForEditing)
                    {
                        case 1:
                            Console.Write("\nВведите новый ID: ");
                            int.TryParse(Console.ReadLine(), out int tempID);
                            item.ID = tempID;
                            Console.WriteLine($"\nИнформация о пользователе:" +
                                              $"\n{item}" +
                                              $"\nУспешно изменена!\n"); ///вывод измененной записи и сообщения об успешном выполнении программы
                            break;

                        case 2:
                            Console.Write("\nВведите новое имя: ");
                            string tempName = Console.ReadLine();
                            item.Name = tempName;
                            Console.WriteLine($"\nИнформация о пользователе:" +
                                              $"\n{item}" +
                                              $"\nУспешно изменена!\n"); ///вывод измененной записи и сообщения об успешном выполнении программы                       
                            break;

                        case 3:
                            Console.Write("\nВведите новый возраст: ");
                            int.TryParse(Console.ReadLine(), out int tempAge);
                            item.Age = tempAge;
                            Console.WriteLine($"\nИнформация о пользователе:" +
                                              $"\n{item}" +
                                              $"\nУспешно изменена!\n"); ///вывод измененной записи и сообщения об успешном выполнении программы               
                            break;

                        case 4:
                            Console.Write("\nВведите новый рост: ");
                            int.TryParse(Console.ReadLine(), out int tempHeight);
                            item.Height = tempHeight;
                            Console.WriteLine($"\nИнформация о пользователе:" +
                                              $"\n{item}" +
                                              $"\nУспешно изменена!\n"); ///вывод измененной записи и сообщения об успешном выполнении программы
                            break;

                        case 5:
                            Console.Write("Введите новую дату рождения: ");
                            DateTime.TryParse(Console.ReadLine(), out DateTime tempBirthDay);
                            item.BirthDay = tempBirthDay;
                            Console.WriteLine($"\nИнформация о пользователе:" +
                                              $"\n{item}" +
                                              $"\nУспешно изменена!\n"); ///вывод измененной записи и сообщения об успешном выполнении программы
                            break;

                        case 6:
                            Console.Write("\nВведите новое место рождения: ");
                            string tempPlaceOfBirth = Console.ReadLine();
                            item.PlaceOfBirth = tempPlaceOfBirth;
                            Console.WriteLine($"\nИнформация о пользователе:\n{item}\nУспешно изменена!\n"); ///вывод измененной записи и сообщения об успешном выполнении программы
                            break;

                        default:

                            Console.Write("\nВведено неверное число!");

                            continue;
                    }
                }
            }
            ReloadToFile(); ///перезапись нового списка в файл
        }

        /// <summary>
        /// Метод вывода коллекции в консоль
        /// </summary>
        public void PrintList()
        {
            foreach (var item in _personList)
            {
                Console.WriteLine(item);
            }
        }


    }
}
