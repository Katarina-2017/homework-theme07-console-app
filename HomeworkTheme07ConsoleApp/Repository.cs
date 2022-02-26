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

        private int idRecord;

        int index; // текущий элемент для добавления в employees

        public Repository(string Path, int IdRecord)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            
            this.index = 0; // текущая позиция для добавления сотрудника в  employees 

            this.idRecord = IdRecord;

            this.employees = new Employee[1]; // инициализаия массива сотрудников.    | изначально предпологаем, что данных нет

            this.GetById(IdRecord);
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
        public void Add(Employee ConcreteEmployee)
        {
            this.Resize(index >= this.employees.Length);
            this.employees[index] = ConcreteEmployee;
            this.index++;
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

                        Add(new Employee(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2], Convert.ToByte(args[3]), Convert.ToInt32(args[4]),
                            Convert.ToDateTime(args[5]),args[6]));
                    }
                }
            }
            else      //иначе выводим соответсвующее сообщение 
            {
                Console.WriteLine($"Файл с именем {this.path} не найден.");
            }
        }

        private void GetById(int idRecord)
        {
            FileInfo userFileName = new FileInfo(this.path);
            if (userFileName.Exists)    //если файл существует, то считываем из него информацию
            {
                using (StreamReader sr = new StreamReader(this.path))
                {
                    bool check=false;
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split('#');

                        if (idRecord == Convert.ToInt32(args[0]))
                        {
                            check=true;
                            Add(new Employee(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2], Convert.ToByte(args[3]), Convert.ToInt32(args[4]),
                            Convert.ToDateTime(args[5]), args[6]));
                        }
                    }
                    if (check == false)
                    {
                        Console.WriteLine($"Записи с таким номером {idRecord} не найдено");
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
            Console.WriteLine($"{"ID",4}\t{"Датa и время записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.employees[i].Print());
            }
        }
    }
}
