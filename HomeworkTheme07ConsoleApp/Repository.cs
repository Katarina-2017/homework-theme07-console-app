using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTheme07ConsoleApp
{
    struct Repository
    {
        private Employee[] employees;

        private string path;

        int index; // текущий элемент для добавления в employees

        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            
            this.index = 0; // инициализаия массива заголовков   
            this.employees = new Employee[1]; // инициализаия массива сотрудников.    | изначально предпологаем, что данных нет

            //this.GetById(); // Загрузка данных
        }

        /// <summary>
        /// Метод увеличения текущего хранилища
        /// </summary>
        /// <param name="Flag">Условие увеличения</param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.employees, this.employees.Length * 2);
            }
        }

        /// <summary>
        /// Метод добавления сотрудника в хранилище
        /// </summary>
        /// <param name="concreteEmployee">Сотрудник</param>
        public void Add(Employee concreteEmployee)
        {
            this.Resize(index >= this.employees.Length);
            this.employees[index] = concreteEmployee;
        }

        private void GetAll()
        {
            FileInfo userFileName = new FileInfo(this.path);
            if (userFileName.Exists)    //если файл существует, то считываем из него информацию
            {
                using (StreamReader sr = new StreamReader(this.path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split('#');

                        //Add(new Employee(args[0], args[1], args[2], Convert.ToUInt32(args[3]), args[4]));
                    }
                }
            }
            else      //иначе выводим соответсвующее сообщение 
            {
                Console.WriteLine($"Файл с именем {this.path} не найден.");
            }
        }

        public void PrintDbToConsole()
        {
            Console.WriteLine($"{"ID",4}\t{"Датa и время записи",12}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",12}\t{"Место рождения"}");

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.employees[i].Print());
            }
        }
    }
}
