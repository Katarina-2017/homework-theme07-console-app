using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeworkTheme07ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            culture.DateTimeFormat.LongDatePattern = "dd.MM.yyyy";
            culture.DateTimeFormat.LongTimePattern = "HH:mm";
            Thread.CurrentThread.CurrentCulture = culture;

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
                case 2:
                    Repository repCreate = new Repository(path);
                    repCreate.Create();
                    repCreate.Save(path); break;
                case 3:
                    Console.WriteLine("Введите номер записи, которую надо удалить:");

                    int recordIdDelete = Convert.ToInt32(Console.ReadLine());

                    Repository repDelete = new Repository(path, recordIdDelete);
                    repDelete.Delete(repDelete);
                    Console.ReadKey();
                    Repository repRewriter = new Repository(path);
                    repRewriter.PrintDbToConsole();

                    break;
                case 4:
                    Console.WriteLine("Введите номер записи, которую надо отредактировать:");

                    int recordIdUpdate = Convert.ToInt32(Console.ReadLine());

                    Repository repUpdate = new Repository(path, recordIdUpdate);
                    repUpdate.Update(repUpdate);
                    
                    break;
                case 5: break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение");
                    break;
            }

           

            Console.ReadLine();
        }
    }
}
