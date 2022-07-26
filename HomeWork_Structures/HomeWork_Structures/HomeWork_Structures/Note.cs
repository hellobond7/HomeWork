using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HomeWork_Structures
{
    internal class Note
    {
        /// <summary>
        /// Коллекция для хранения пользователей
        /// </summary>
        public List<Person> PersonList;

        /// <summary>
        /// Переменная для хранения информации о пользователе
        /// </summary>
        private Person person;

        /// <summary>
        /// Путь к файлу с данными
        /// </summary>
        private string path;

        /// <summary>
        /// Количество пользователей в записной книжке
        /// </summary>
        private int ID;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Note(string path)
        {
            this.path = path;
            CheckFile();
            this.ID = CountOfPerson();
            PersonList = new List<Person>();
        }

        /// <summary>
        /// Метод добавления нового пользователя
        /// </summary>
        private void AddPerson()
        {
            ID++; ///увеличение счетчика
            person = new Person(ID); /// инициализация переменной типа Person
            LoadToFile(person);
        }

        /// <summary>
        /// Метод записи добавления информации о новом пользователе в текстовый файл
        /// </summary>
        /// <param name="newPerson">Новый пользователь</param>
        private void LoadToFile(Person newPerson)
        {
            using (StreamWriter strWriter = new StreamWriter(path, true, Encoding.Unicode)) ///объявление потока записи
                strWriter.WriteLine(newPerson.ToString()); ///запись информации о пользователе в файл
        }

        /// <summary>
        /// Метод перезаписи информации о пользователях в текстовый файл
        /// </summary>
        /// <param name="PersonList">Коллекция хранящая список пользователей</param>
        private void ReloadToFile(List<Person> PersonList)
        {
            using (StreamWriter strWriter = new StreamWriter(path, false, Encoding.Unicode)) ///объявление потока записи
                foreach (var dir in PersonList)
                {
                    strWriter.WriteLine(dir.ToString()); ///запись строки в файл
                }
        }

        /// <summary>
        /// Метод проверки существования файла. Если файла не существует - то он создается
        /// </summary>
        private void CheckFile()
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists == false)
            {
                fileInf.Create().Close(); ///создание пустого файла
            }
        }
        
        /// <summary>
        /// Метод чтения записей из файла и запись в коллекцию для дальнейшего взаимодействия
        /// </summary>
        /// <param name="PersonList"></param>
        private void ReadFromFileForEditing(List<Person> PersonList)
        {
            char[] delimiter = new char[] { '\t' }; /// инициализация массива разделителей
            PersonList.Clear();
            using (StreamReader strReader = new StreamReader(path)) ///объявление потока чтения
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
                    PersonList.Add(person);
                }
        }

        /// <summary>
        /// Метод чтения информации о пользователе из файла
        /// </summary>
        private void ReadFromFile()
        {
            CheckFile();
            int count = 0; ///инициализация переменной счетчика количества записей в записной книжке
            using (StreamReader strReader = new StreamReader(path)) ///объявление потока чтения
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
        private int CountOfPerson()
        {
            int count = 0;///инициализация переменной счетчика количества записей в записной книжке
            using (StreamReader strReader = new StreamReader(path)) ///объявление потока чтения
                while (!strReader.EndOfStream)
                {
                    strReader.ReadLine(); ///чтение строки из файла
                    count++; ///увеличение счетчика
                }
            if (count == 0)
            {
                Console.WriteLine("В записной книжке нет ни одной записи!"); ///вывод сообщения, если файл пустой
            }
            return count;
        }

        /// <summary>
        /// Удаления информации о пользователе по его ID
        /// </summary>
        /// <param name="PersonList">Коллекция хранящая информацию о пользователях</param>
        /// <param name="ID">ID пользователя информацию о котором необходимо удалить</param>
        private void RemoveEntryFromFile(List<Person> PersonList, int ID)
        {
            ReadFromFileForEditing(PersonList); ///вывод на экран полного списка пользователей
            int count = 0; ///инициализация счетчика

            foreach (var item in PersonList)
            {
                if (item.ID == ID)
                {
                    PersonList.RemoveAt(count); ///удаление записи из коллекции
                    Console.WriteLine($"Информация о пользователе:\n{item}\nУспешно удалена!\n"); ///вывод удаляемой
                                                                                                  ///и сообщения об успешном выполнении программы
                    break;
                }
                count++;
            }
            ReloadToFile(PersonList); ///перезапись нового списка в файл
        }

        /// <summary>
        /// Возврат на главный экран меню
        /// </summary>
        private void ReturnToMenu(out char toMenu)
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
        private void RepeatAction(out char toContinue)
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
        /// <param name="PersonList">Коллекция хранящая информацию о пользователях</param>
        /// <param name="rangeStart">Начало диапазона</param>
        /// <param name="endOfRange">Конец диапазона</param>
        private void SortNote(List<Person> PersonList, DateTime rangeStart, DateTime endOfRange)
        {
            foreach (var item in PersonList)
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
        /// <param name="PersonList">Коллекция хранящая информацию о пользователях</param>
        /// <param name="ID">ID пользователя информацию о котором необходимо изменить</param>
        private void UserEditing(List<Person> PersonList, int ID, byte ChoiseForEditing)
        {
            foreach (var item in PersonList)
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
            ReloadToFile(PersonList); ///перезапись нового списка в файл
        }

        /// <summary>
        /// Вызов меню
        /// </summary>
        /// <param name="path">Файл для сохранения записной книжки</param>
        public void Menu(FileInfo path)
        {
            Console.Clear();
            char toContinue; ///переменная для выбора действия для продолжения действий внутри меню
            char toMenu = 'y'; ///переменная возврата в меню
            this.path = path.ToString();
            while (true)
            {
                //Note Notebook = new Note(fileInf.ToString());
                if (char.ToLower(toMenu) == 'y')
                {
                    Console.Clear(); ///очистка экрана

                    ///основное меню
                    Console.Write("1 - Просмотр записи:" +
                                "\n2 - Создание записи" +
                                "\n3 - Удаление записи" +
                                "\n4 - Редактирование записи" +
                                "\n5 - Загрузка записей в выбранном диапазоне дат" +
                                "\n6 - Сортировка записей" +
                                "\nВыбор: ");
                    sbyte.TryParse(Console.ReadLine(), out sbyte choise); ///choise - переменная для выбора действий в основном меню
                    Console.Clear(); ///очистка экрана

                    switch (choise)
                    {
                        /*ПРОСМОТР ЗАПИСИ*/
                        case 1:

                            ReadFromFile();///вызов метода чтения информации о пользователях из файла
                            ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран

                            continue;

                        /*СОЗДАНИЕ ЗАПИСИ*/
                        case 2:
                            do
                            {
                                Console.Clear(); ///очистка экрана

                                AddPerson(); ///запись нового пользователя

                                ///результат выполнения действия
                                //Console.WriteLine($"Данные записаны во внешний файл!" +
                                //                $"\nФайл можно найти по адресу:{fileInf.DirectoryName}");
                                Console.WriteLine($"Данные записаны во внешний файл!" +
                                                $"\nФайл можно найти по адресу:{path.DirectoryName}");

                                Console.Write($"\nДобавить нового пользователя? y/n "); ///запрос повторения
                                char.TryParse(Console.ReadLine(), out toContinue);

                            } while (char.ToLower(toContinue) == 'y');

                            ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                            continue;

                        /*УДАЛЕНИЕ ЗАПИСИ*/
                        case 3:
                            //StreamReaderFromFile(); ///вызов метода чтения информации о пользователях из файла

                            ReadFromFile(); ///вызов метода чтения информации о пользователях из файла
                            Console.Write("\n1 - Очистить список" +
                                          "\n2 - Выборочное удаление" +
                                          "\nВыбор: ");
                            byte.TryParse(Console.ReadLine(), out byte choiseDel);
                            switch (choiseDel)
                            {
                                case 1:
                                    path.Delete(); ///удаление файла
                                    Console.WriteLine("Список очищен!");
                                    //System.Threading.Thread.Sleep(5000); /// задержка на 5 сек
                                    //ReadFromFile(); ///вызов метода чтения информации о пользователях из файла

                                    ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                    continue;

                                case 2:
                                    do
                                    {
                                        Console.Write($"\nКакую запись вы хотите удалить?" +
                                                      $"\nВведите ID:");
                                        int.TryParse(Console.ReadLine(), out int ID);

                                        //RemoveEntryFromFile(PersonList, ID); ///вызов метода удаления записи из файла
                                        RemoveEntryFromFile(PersonList, ID); ///вызов метода удаления записи из файла

                                        //StreamReaderFromFile(); ///вызов метода чтения информации о пользователях из файла
                                        ReadFromFile(); ///вызов метода чтения информации о пользователях из файла

                                        Console.Write($"\nПродолжить удаление? y/n "); ///запрос повторения
                                        char.TryParse(Console.ReadLine(), out toContinue);

                                    } while (char.ToLower(toContinue) == 'y');

                                    ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                    continue;
                                default:
                                    Console.Write("Не выбран ни один из предложенных вариантов. " +
                                                "\nПродолжить? y/n ");
                                    char.TryParse(Console.ReadLine(), out toMenu);
                                    continue;
                            }

                        /*РЕДАКТИРОВАНИЕ ЗАПИСИ*/
                        case 4:

                            if (CountOfPerson() == 0)
                            {
                                Console.WriteLine("В записной книжке нет записей. Сделайте первую запись");
                                ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                continue;
                            }
                            else
                            {
                                do
                                {
                                    Console.Clear(); ///очистка экрана

                                    ReadFromFile(); ///вызов метода чтения информации о пользователях из файла
                                    ReadFromFileForEditing(PersonList);

                                    Console.Write($"\nНапишите ID пользователя информацию о котором вы хотите изменить: ");
                                    bool suc = int.TryParse(Console.ReadLine(), out int choiseID); ///инициализация переменной выбора ID
                                    if (!suc)
                                    {
                                        Console.Write("\nНе выбран ни один вариант из списка!" +
                                                      "\nПовторите ввод. Нажмите любую клавишу!");
                                        Console.ReadKey();
                                        toContinue = 'y';
                                        continue;
                                    }

                                    Console.Write($"\nКакое поле вы хотите изменить?" +
                                                  $"\n1 - ID" +
                                                  $"\n2 - Имя пользователя" +
                                                  $"\n3 - Возраст" +
                                                  $"\n4 - Рост" +
                                                  $"\n5 - Дата рождения" +
                                                  $"\n6 - Место рождения");
                                    Console.Write("\nВыбор: ");
                                    byte.TryParse(Console.ReadLine(), out byte choiseForEditing);

                                    UserEditing(PersonList, choiseID, choiseForEditing); ///вызов метода редактирования записи

                                    Console.Write($"Хотите изменить что-то еще? y/n "); ///запрос повторения
                                    char.TryParse(Console.ReadLine(), out toContinue);

                                } while (char.ToLower(toContinue) == 'y');

                                ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                continue;
                            }

                        /*ЗАГРУЗКА ЗАПИСЕЙ В ВЫБРАННОМ ДИАПАЗОНЕ ДАТ*/
                        case 5:
                            ReadFromFile(); ///вызов метода чтения информации о пользователях из файла

                            Console.Write("В каком диапазоне дат вы хотите посмотреть записи?" +
                                        "\nФормат ввода даты - ДД.ММ.ГГГГ");

                            Console.Write("\nНачало диапазона: ");
                            DateTime.TryParse(Console.ReadLine(), out DateTime rangeStart);
                            Console.Write("Конец диапазона: ");
                            DateTime.TryParse(Console.ReadLine(), out DateTime endOfRange);

                            SortNote(PersonList, rangeStart, endOfRange); ///сортировка списка по выбранному диапазону дат
                            ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран

                            continue;

                        /*СОРТИРОВКА ЗАПИСЕЙ*/
                        case 6:

                            do
                            {
                                Console.Clear(); ///очистка экрана

                                ReadFromFile(); ///вызов метода чтения информации о пользователях из файла
                                ReadFromFileForEditing(PersonList); ///вызов метода для вывода на экран полного списка пользователей

                                Console.Write($"\nПо какому полю хотите отсортировать список?" +
                                              $"\n1 - ID" +
                                              $"\n2 - Дата создания" +
                                              $"\n3 - Имя пользователя" +
                                              $"\n4 - Возраст" +
                                              $"\n5 - Рост" +
                                              $"\n6 - Дата рождения" +
                                              $"\n7 - Место рождения");
                                Console.Write("\nВыбор: ");
                                byte.TryParse(Console.ReadLine(), out byte choise1);

                                PersonList.Sort(new Sort(choise1)); ///сортировка списка по выбранному значению

                                foreach (var item in PersonList)
                                {
                                    Console.WriteLine(item);
                                }

                                RepeatAction(out toContinue); ///вызов метода с запросов повтора действия

                            } while (char.ToLower(toContinue) == 'y');

                            ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                            continue;

                        /*ВЫВОД ДЕФОЛТНОГО ЗНАЧЕНИЯ*/
                        default:
                            Console.Write("Не выбран ни один из предложенных вариантов. Продолжить? y/n");
                            char.TryParse(Console.ReadLine(), out toMenu);
                            continue;
                    }
                }
                break;
            }
        }
    }
}
