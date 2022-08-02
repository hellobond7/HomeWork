using System;
using System.IO;

namespace HomeWork_Structures
{
    /*
     Улучшите программу, которую разработали в модуле 6. Создайте структуру «Сотрудник» со следующими полями:

    ID
    Дата и время добавления записи
    Ф.И.О.
    Возраст
    Рост
    Дата рождения
    Место рождения

    Для записей реализуйте следующие функции:

    • Просмотр записи. Функция должна содержать параметр ID записи, которую необходимо вывести на экран. 
    • Создание записи.
    • Удаление записи.
    • Редактирование записи.
    • Загрузка записей в выбранном диапазоне дат.
    • Сортировка по возрастанию и убыванию даты.
     */

    internal class Program
    {
        static void Main(string[] args)
        {
            FileInfo fileAdressForNotebook = new FileInfo(@"Notebook.csv");
            FileInfo fileAdressForCountOfPerson = new FileInfo(@"CountOfPerson.csv");
            Menu menu = new Menu(fileAdressForNotebook,fileAdressForCountOfPerson);
            menu.MainMenu();
            Console.ReadKey();
        }
    }
}
