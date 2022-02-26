using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTheme07ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"db.txt";

            Console.WriteLine($"Добро пожаловать в ежедневник. \nВыберите одну из следующих функций:" +
                $"\n1 - Просмотр записи;" +
                $"\n2 - Создание записи;" +
                $"\n3 - Удаление записи;" +
                $"\n4 - Редактирование записи;" +
                $"\n5 - Загрузка записей в выбранном диапазоне дат;" +
                $"\n6 - Сортировка по возрастанию и убыванию даты.");

            byte userOption = Convert.ToByte(Console.ReadLine());

            switch (userOption)
            {
                case 1:
                    Console.WriteLine("Введите номер записи:");

                    int recordID = Convert.ToInt32(Console.ReadLine());

                    Repository rep = new Repository(path, recordID);

                    rep.PrintDbToConsole();  break; // Загрузка данных
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение");
                    break;
            }

           

            Console.ReadLine();
        }
    }
}
