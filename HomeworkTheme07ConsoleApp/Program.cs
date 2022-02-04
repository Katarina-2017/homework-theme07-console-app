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
            Employee newEmployee = new Employee(1, "Ivanov Ivan Ivanovich", 33, 170, new DateTime(1988, 7, 20),
                "Moscow");
            Console.WriteLine(newEmployee.Print());

            Console.ReadLine();
        }
    }
}
