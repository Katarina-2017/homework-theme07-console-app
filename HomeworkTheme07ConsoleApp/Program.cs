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

            Repository rep = new Repository(path);

            rep.PrintDbToConsole();

            Console.ReadLine();
        }
    }
}
