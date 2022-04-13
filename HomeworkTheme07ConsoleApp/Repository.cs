using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTheme07ConsoleApp
{
    /// <summary>
    /// Структура по работе с данными
    /// </summary>
    struct Repository
    {
        // Список сотрудников (частный вспомогательный список)
        private List<Employee> _employees { get; set; }

        private string path; // Путь к файлу с данными

        private int idRecord; // Идентификатор записи


        #region Конструкторы
        /// <summary>
        /// Создание репозитория
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными

            this.idRecord = 0; // Текущий индентификатор записи

            _employees = new List<Employee>(); // Инициализация списка сотрудников.    | изначально предпологаем, что данных нет

            _employees = GetEmployeesFromTxt(); // Получаем всех сотрудников
        }

        /// <summary>
        /// Создание репозитория
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
        /// <param name="IdRecord">Идентификатор записи</param>
        public Repository(string Path, int IdRecord)
        {
            this.path = Path;
            
            this.idRecord = IdRecord;

            _employees = new List<Employee>();

            _employees = GetEmployeesFromTxt();

            Console.WriteLine($"{"ID",4}\t{"Датa и время добавления записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");
            Console.WriteLine(GetById(idRecord).Print()); // Получаем только одного сотрудника
        }

        /// <summary>
        /// Загрузка записей в выбранном диапазоне дат
        /// </summary>
        /// <param name="Path">Путь к файлу с данным</param>
        /// <param name="dateStart">Начальная дата</param>
        /// <param name="dateEnd">Конечная дата</param>
        public Repository (string Path, DateTime dateStart, DateTime dateEnd)
        {
            this.path = Path;

            this.idRecord = 0;

            _employees = new List<Employee>();

            _employees = GetEmployeesFromTxt(); // Получаем всех сотрудников

            Console.WriteLine($"{"ID",4}\t{"Датa и время добавления записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");

            if (dateStart < dateEnd) // Начальная дата должна быть меньше конечной даты диапазона
            {
                foreach (var item in _employees)
                {
                    if (item.RecordCreationDate >= dateStart && item.RecordCreationDate <= dateEnd) 
                    {
                        Console.WriteLine(item.Print()); 
                    }
                }
            }
            else if (dateStart > dateEnd) // если начальная дата больше конечной даты, выводим сообщение об ошибке
            {
                Console.WriteLine("Вы задали некорректный диапазон дат");
            }
        }

        /// <summary>
        /// Сортировка по возрастанию и убыванию даты
        /// </summary>
        /// <param name="Path">Путь к файлу с данным</param>
        /// <param name="userWay">Способ сортировки: 1 - по возрастанию, 2 - по убыванию</param>
        public Repository (string Path, byte userWay)
        {
            this.path = Path;

            this.idRecord = 0;

            _employees = new List<Employee>();

            _employees = GetEmployeesFromTxt();

            switch (userWay)
            {
                case 1:
                    var sortedEmploeyees = _employees.OrderBy(e => e.RecordCreationDate).ToList();

                    PrintDbToConsole(sortedEmploeyees);
                    Save(path, sortedEmploeyees);
                    break;
                case 2:
                    var sortedEmploeyeesDescending = _employees.OrderByDescending(e => e.RecordCreationDate).ToList();

                    PrintDbToConsole(sortedEmploeyeesDescending);
                    Save(path, sortedEmploeyeesDescending);
                    break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение");
                    break;
            }
        }
        #endregion

        #region Методы
        // Получаем список всех сотрудников из файла (парсинг файла, частный метод)
        private List<Employee> GetEmployeesFromTxt()
        {
            FileInfo userFileName = new FileInfo(this.path);

            if (userFileName.Exists)
            {
                var employees = new List<Employee>();

                string[] employeesTxt = File.ReadAllLines(this.path);
                foreach (string employeeTxtString in employeesTxt)
                {
                    var employee = GetEmployeeFromTxtString(employeeTxtString);
                    employees.Add(employee);

                }

                return employees;
            }
            else
            {
                var employeesNull = new List<Employee>(); ;
                Console.WriteLine($"Файл с именем {this.path} не найден.");
                return employeesNull;
            }
        }

        // Получаем сотрудника из строки (парсинг сотрудника, частный метод)
        private Employee GetEmployeeFromTxtString(string employeeTxtString)
        {

                string[] args = employeeTxtString.Split('#');
                if (args[0] !="")
                {
                    int id = int.Parse(args[0]);
                    DateTime createdAt = DateTime.ParseExact(args[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                    string fullName = args[2];
                    byte age = byte.Parse(args[3]);
                    int height = int.Parse(args[4]);
                    DateTime birthDate = DateTime.ParseExact(args[5], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    string birthPlace = args[6];

                    var employee = new Employee(
                        id,
                        createdAt,
                        fullName,
                        age,
                        height,
                        birthDate,
                        birthPlace
                        );

                    return employee;
                }
                else
                {
                    var employeeNull = new Employee();
                    Console.WriteLine($"Файл {this.path} пуст");
                    return employeeNull;
                }
        }

        /// <summary>
        /// Метод GetAll() - получает всех сотрудников
        /// </summary>
        public List<Employee> GetAll()
        {
            return _employees;
        }

        /// <summary>
        /// Метод GetById(int idRecord) - получает сотрудника по идентификатору
        /// </summary>
        /// <param name="idRecord">Идентификатор записи</param>
        public Employee GetById(int idRecord)
        {
            var employee = _employees
                .FirstOrDefault(e => e.Id == idRecord);
            return employee;
        }

        /// <summary>
        /// Метод Create() - создает нового сотрудника
        /// </summary>
        public void Create()
        {
            char key = 'д';
            _employees = _employees.OrderBy(e => e.Id).ToList();

            int noteId = _employees.Last().Id; // Сохраняем последний Id сотрудника из списка

            do
            {
                noteId++; // Определяем ID новой записи автоматически, исходя из последнего Id сотрудника в списке
                Console.WriteLine($"\nID записи: {noteId}");

                string noteDate = DateTime.Now.ToString();
                Console.WriteLine($"Дата и время добавления записи: {noteDate}");

                Console.WriteLine("Ф. И. О.: ");
                string noteInitials = Console.ReadLine();

                Console.WriteLine("Возраст: ");
                byte noteAge = Convert.ToByte(Console.ReadLine());

                Console.WriteLine("Рост: ");
                int noteHeight = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Датa рождения: ");
                string noteDateOfBirth = Console.ReadLine();

                Console.WriteLine("Место рождения: ");
                string noteBirthPlace = Console.ReadLine();

                _employees.Add(new Employee(noteId, Convert.ToDateTime(noteDate), noteInitials, noteAge, noteHeight, Convert.ToDateTime(noteDateOfBirth), noteBirthPlace)); // Добавляем сотрудника в массив
                Console.WriteLine("Продолжить н/д"); key = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(key) == 'н');

            PrintDbToConsole(_employees);

            Save(this.path, _employees);
        }

        /// <summary>
        /// Метод Delete(Employee employee) - удаляет заданного сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник для удаления</param>
        public void Delete(Employee employee)
        {
            var oldEmployee = _employees
                .FirstOrDefault(e => e.Id == employee.Id);

            _employees.Remove(oldEmployee);

            PrintDbToConsole(_employees);

            Save(this.path, _employees);
        }

        /// <summary>
        /// Метод Update(Employee employee) - редактирует заданного сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник для редактирования</param>
        public void Update(Employee employee)
        {
            var oldEmployee = _employees
                .FirstOrDefault(e => e.Id == employee.Id);

            _employees.Remove(oldEmployee);
            Console.WriteLine("Введите новые значения полей:");
            Console.WriteLine($"\nID записи: {employee.Id}"); // Идентификатор записи не изменяем, всю остальную информацию редактируем свободно
            int noteId = employee.Id;

            Console.WriteLine($"Дата и время добавления записи:");
            DateTime noteDateUpdate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Ф. И. О.: ");
            string noteFullName = Console.ReadLine();

            Console.WriteLine("Возраст: ");
            byte noteAge = Convert.ToByte(Console.ReadLine());

            Console.WriteLine("Рост: ");
            int noteHeight = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Датa рождения: ");
            DateTime noteDateOfBirth = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Место рождения: ");
            string noteBirthPlace = Console.ReadLine();

            _employees.Add(new Employee(noteId,noteDateUpdate,noteFullName,noteAge,noteHeight, noteDateOfBirth,noteBirthPlace));

            PrintDbToConsole(_employees); 

            Save(this.path, _employees);
        }

        /// <summary>
        /// Save(string Path, List<Employee> employeeList) - перезаписывает измененные данные в файл
        /// </summary>
        /// <param name="Path">Путь к исходному файлу</param>
        /// <param name="employeeList">Список сотрудников</param>
        public void Save(string Path, List<Employee> employeeList)
        {
            FileInfo userFileName = new FileInfo(path);

            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < employeeList.Count; i++)
                {
                    string temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", // Формирует строки в определенном формате через #
                                            employeeList[i].Id,
                                            employeeList[i].RecordCreationDate,
                                            employeeList[i].InitialsEmployee,
                                            employeeList[i].Age,
                                            employeeList[i].Height,
                                            employeeList[i].DateOfBirth.ToShortDateString(),
                                            employeeList[i].BirthPlace);
                    sw.WriteLine($"{temp}"); // Записываем эту строку в исходный файл
                }
            }
        }

        /// <summary>
        /// Метод PrintDbToConsole(List<Employee> employeeList) - выводит полную информацию о сотруднике на экран
        /// </summary>
        /// <param name="employeeList">Список сотрудников</param>
        public void PrintDbToConsole(List<Employee> employeeList)
        {
            Console.WriteLine($"{"ID",4}\t{"Датa и время добавления записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");

            foreach (var employee in employeeList)
            {
                Console.WriteLine(employee.Print());
            }
        }
        #endregion
    }
}