using System;
using System.Collections.Generic;
using System.IO;


namespace HomeWork_Structures
{
    class Menu
    {
        /// <summary>
        /// Путь к файлу с данными
        /// </summary>
        private FileInfo _fileAdressForNotebook;

        /// <summary>
        /// Путь к файлу с данными о количестве пользователей
        /// </summary>
        private FileInfo _fileAdressForCountOfPerson;

        /// <summary>
        /// Переменная класса Note, вводимая для взамодействия с записями в записной книжке
        /// </summary>
        private Note _notebook;

        /// <summary>
        /// Коллекция для хранения пользователей
        /// </summary>
        private List<Person> _personList;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileAdressForNotebook">Путь к файлу с данными</param>
        public Menu(FileInfo fileAdressForNotebook, FileInfo fileAdressForCountOfPerson)
        {
            _fileAdressForNotebook = fileAdressForNotebook;
            _fileAdressForCountOfPerson = fileAdressForCountOfPerson;
            _notebook = new Note(_fileAdressForNotebook, _fileAdressForCountOfPerson);
            _personList = new List<Person>();
        }

        /// <summary>
        /// Вызов меню
        /// </summary>
        public void MainMenu()
        {
            Console.Clear(); ///очистка экрана
            char toContinue; ///переменная для выбора действия для продолжения действий внутри меню
            char toMenu = 'y'; ///переменная возврата в меню

            while (true)
            {
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
                            _notebook.ReadFileAndPrintToConsole();///вызов метода чтения информации о пользователях из файла
                            _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                            continue;

                        /*СОЗДАНИЕ ЗАПИСИ*/
                        case 2:
                            do
                            {
                                Console.Clear(); ///очистка экрана
                                _notebook.AddPerson(); ///запись нового пользователя
                                Console.WriteLine($"\nДанные записаны во внешний файл!"); ///результат выполнения действия
                                _notebook.RepeatAction(out toContinue); ///вызов метода с запросом повтора действия
                            } while (char.ToLower(toContinue) == 'y');

                            _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                            continue;

                        /*УДАЛЕНИЕ ЗАПИСИ*/
                        case 3:
                            _notebook.ReadFileAndPrintToConsole(); ///вызов метода чтения информации о пользователях из файла
                            Console.Write("\n1 - Очистить список" +
                                          "\n2 - Выборочное удаление" +
                                          "\nВыбор: ");
                            byte.TryParse(Console.ReadLine(), out byte choiseDel);
                            switch (choiseDel)
                            {
                                /*ОЧИСТКА СПИСКА*/
                                case 1:
                                    _fileAdressForNotebook.Delete(); ///удаление файла со списком пользователей
                                    _fileAdressForCountOfPerson.Delete(); ///удаление файла с колчеством записей в записной книжке
                                    Console.WriteLine("Список очищен!");

                                    _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                    continue;
                                /*УДАЛЕНИЕ ВЫБРАННОЙ ЗАПИСИ*/
                                case 2:
                                    do
                                    {
                                        Console.Write($"\nКакую запись вы хотите удалить?" +
                                                      $"\nВведите ID:");
                                        int.TryParse(Console.ReadLine(), out int ID);

                                        _notebook.RemoveEntryFromFile(ID); ///вызов метода удаления записи из файла
                                        _notebook.ReadFileAndPrintToConsole(); ///вызов метода чтения информации о пользователях из файла
                                        _notebook.RepeatAction(out toContinue); ///вызов метода с запросом повтора действия

                                    } while (char.ToLower(toContinue) == 'y');

                                    _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                    continue;
                                default:
                                    Console.Write("Не выбран ни один из предложенных вариантов. " +
                                                "\nПродолжить? y/n ");
                                    char.TryParse(Console.ReadLine(), out toMenu);
                                    continue;
                            }

                        /*РЕДАКТИРОВАНИЕ ЗАПИСИ*/
                        case 4:

                            if (_notebook.CountOfPerson() == 0)
                            {
                                Console.WriteLine("В записной книжке нет записей. Сделайте первую запись");
                                _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                continue;
                            }
                            else
                            {
                                do
                                {
                                    Console.Clear(); ///очистка экрана

                                    _notebook.ReadFileAndPrintToConsole(); ///вызов метода чтения информации о пользователях из файла
                                    _notebook.ReadFileForEditing();

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

                                    _notebook.UserEditing(choiseID, choiseForEditing); ///вызов метода редактирования записи

                                    _notebook.RepeatAction(out toContinue); ///вызов метода с запросом повтора действия

                                } while (char.ToLower(toContinue) == 'y');

                                _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
                                continue;
                            }

                        /*ЗАГРУЗКА ЗАПИСЕЙ В ВЫБРАННОМ ДИАПАЗОНЕ ДАТ*/
                        case 5:
                            _notebook.ReadFileAndPrintToConsole(); ///вызов метода чтения информации о пользователях из файла

                            Console.Write("В каком диапазоне дат вы хотите посмотреть записи?" +
                                        "\nФормат ввода даты - ДД.ММ.ГГГГ");

                            Console.Write("\nНачало диапазона: ");
                            DateTime.TryParse(Console.ReadLine(), out DateTime rangeStart);
                            Console.Write("Конец диапазона: ");
                            DateTime.TryParse(Console.ReadLine(), out DateTime endOfRange);

                            _notebook.SortNote(rangeStart, endOfRange); ///сортировка списка по выбранному диапазону дат
                            _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран

                            continue;

                        /*СОРТИРОВКА ЗАПИСЕЙ*/
                        case 6:
                            do
                            {
                                Console.Clear(); ///очистка экрана

                                _notebook.ReadFileAndPrintToConsole(); ///вызов метода чтения информации о пользователях из файла
                                _notebook.ReadFileForEditing(); ///вызов метода для вывода на экран полного списка пользователей

                                Console.Write($"\nПо какому полю хотите отсортировать список?" +
                                              $"\n1 - ID" +
                                              $"\n2 - Дата создания" +
                                              $"\n3 - Имя пользователя" +
                                              $"\n4 - Возраст" +
                                              $"\n5 - Рост" +
                                              $"\n6 - Дата рождения" +
                                              $"\n7 - Место рождения");
                                Console.Write($"\nВыбор: ");
                                byte.TryParse(Console.ReadLine(), out byte choise1);

                                _personList.Sort(new Sort(choise1)); ///сортировка списка по выбранному значению
                                _notebook.PrintList(); ///вызов метода для вывода коллекции в консоль
                                _notebook.RepeatAction(out toContinue); ///вызов метода с запросом повтора действия

                            } while (char.ToLower(toContinue) == 'y');

                            _notebook.ReturnToMenu(out toMenu); ///вызов метода возврата на главный экран
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
