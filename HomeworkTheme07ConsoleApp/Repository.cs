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

            _employees = new List<Employee>(); // Инициализация массива сотрудников.    | изначально предпологаем, что данных нет

            _employees = GetEmployeesFromTxt();
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

            PrintDbToConsole(GetById(idRecord));
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

            this.GetAll(); // Получаем всех сотрудников

            //if (dateStart < dateEnd) // Начальная дата должна быть меньше конечной даты диапазона
            //{
            //    Console.WriteLine($"{"ID",4}\t{"Датa и время записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");

            //    for (int i = 0; i < index; i++)
            //    {
            //        if (this.employees[i].RecordCreationDate >= dateStart && this.employees[i].RecordCreationDate <= dateEnd) // Если дата создания записи попадает в заданный диапазон
            //        {
            //            Console.WriteLine(this.employees[i].Print()); // Выводим массив сотрудников на экран
            //        }
            //    }
            //}
            //else if (dateStart > dateEnd) // если начальная дата больше конечной даты, выводим сообщение об ошибке
            //{
            //    Console.WriteLine("Вы задали некорректный диапазон дат");
            //}
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

            //this.GetAll(); // Получаем всех сотрудников

            //switch (userWay)
            //{
            //    case 1:
            //        // сортировка пузырьком
            //        Employee tmp;

            //        for (int i = index - 1; i >= 0; i--)
            //        {
            //            for (int j = 0; j < i; j++)
            //            {
            //                // сравниваем элементы массива сотрудников по дате и времени создания записи
            //                if (employees[j].RecordCreationDate > employees[j + 1].RecordCreationDate)
            //                {
            //                    tmp = employees[j];
            //                    employees[j] = employees[j + 1];
            //                    employees[j + 1] = tmp;
            //                }
            //            }
            //        }
            //        break;
            //    case 2:
            //        // сортировка пузырьком
            //        Employee itm;

            //        for (int i = index - 1; i >= 0; i--)
            //        {
            //            for (int j = 0; j < i; j++)
            //            {
            //                // сравниваем элементы массива сотрудников по дате и времени создания записи
            //                if (employees[j].RecordCreationDate < employees[j + 1].RecordCreationDate)
            //                {
            //                    itm = employees[j];
            //                    employees[j] = employees[j + 1];
            //                    employees[j + 1] = itm;
            //                }
            //            }
            //        }
            //        break;
            //    default:
            //        Console.WriteLine("Вы ввели некорректное значение");
            //        break;
            //}
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
        public List<Employee> GetById(int idRecord)
        {
            var employee = new List<Employee>();

            foreach (var value in _employees)
            {
                if (value.Id == idRecord)
                {
                    employee.Add(value);
                }
            }
            return employee;
        }

        /// <summary>
        /// Метод Create() - создает нового сотрудника
        /// </summary>
        public void Create()
        {
            char key = 'д';
            int noteId = _employees.Count; // Сохраняем количество элементов массива-хранилища

            do
            {
                noteId++; // Определяем ID новой записи автоматически, исходя из количества элементов в массиве
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

            PrintDbToConsole(_employees); // Выводим массив сотрудников на экран

            Save(this.path, _employees);
        }

        /// <summary>
        /// Метод Delete(Repository employees) - удаляет заданного сотрудника
        /// </summary>
        /// <param name="employee">Массив сотрудников для удаления</param>
        public void Delete(Repository employee)
        {
            //string temp = "";
            
            ////bool check = false;
            //for (int k = 0; k < this.index; k++)
            //{
            //    //bool check = false; // Переменная для проверки удаления записи
            //    temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", // Формируем временную строку в определенном формате из конкретного сотрудника, которого необходимо удалить
            //                            this.employees[k].Id,
            //                            this.employees[k].RecordCreationDate,
            //                            this.employees[k].InitialsEmployee,
            //                            this.employees[k].Age,
            //                            this.employees[k].Height,
            //                            this.employees[k].DateOfBirth.ToShortDateString(),
            //                            this.employees[k].BirthPlace);
            //}
            
            //if (temp != "") // Дополнительная проверка на пустоту строки
            //    {
            //        Console.WriteLine("Вы выбрали эту запись для удаления:");
            //        Console.WriteLine(temp);
            //    for (int i = 0; i < this.index; i++)
            //    {
            //        if (String.Equals(this.employees[i], temp) == true)
            //        {
            //            Console.WriteLine(employees[i].Print());
            //        }
            //    }

                //if (check == true) // Если удаление прошло успешно, то выводим соответствующее сообщение
                //{
                //    Console.WriteLine("Запись успешно удалена!");
                //}
                //else // Если строки не совпали и удаление не прошло, выводим соответствующее сообщение
                //{
                //    Console.WriteLine("Что-то пошло не так...Запись не удалена!");
                //}
            //}
        }

        /// <summary>
        /// Метод Update(Repository employees) - редактирует заданного сотрудника
        /// </summary>
        /// <param name="employees">Массив сотрудников для редактирования</param>
        public void Update(Repository employees)
        {
            //string temp = "";
            //int noteId = 0;

            //for (int i = 0; i < this.index; i++)
            //{
            //    temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", // Формируем временную строку в определенном формате из конкретного сотрудника, которого необходимо изменить
            //                            this.employees[i].Id,
            //                            this.employees[i].RecordCreationDate,
            //                            this.employees[i].InitialsEmployee,
            //                            this.employees[i].Age,
            //                            this.employees[i].Height,
            //                            this.employees[i].DateOfBirth,
            //                            this.employees[i].BirthPlace);
            //    noteId = this.employees[i].Id; // Сохраняем идентификатор этой записи

            //    if (temp != "") // // Дополнительная проверка на пустоту строки
            //    {
            //        Console.WriteLine("Вы выбрали эту запись для редактирования:");
            //        Console.WriteLine(temp);

            //        string note = string.Empty;

            //        Console.WriteLine("Введите новые значения полей:");
            //        Console.WriteLine($"\nID записи: {noteId}"); // Идентификатор записи не изменяем, всю остальную информацию редактируем свободно
            //        note += $"{noteId}" + "#";

            //        Console.WriteLine($"Дата и время добавления записи:");
            //        note += $"{Console.ReadLine()}" + "#";

            //        Console.WriteLine("Ф. И. О.: ");
            //        note += $"{Console.ReadLine()}" + "#";

            //        Console.WriteLine("Возраст: ");
            //        note += $"{Console.ReadLine()}" + "#";

            //        Console.WriteLine("Рост: ");
            //        note += $"{Console.ReadLine()}" + "#";

            //        Console.WriteLine("Датa рождения: ");
            //        note += $"{Console.ReadLine()}" + " 00:00" + "#";

            //        Console.WriteLine("Место рождения: ");
            //        note += $"{Console.ReadLine()}";

            //        using (StreamReader reader = new StreamReader(this.path))
            //        {
            //            using (StreamWriter writer = new StreamWriter(@"db_temp.txt")) // Создаем временный файл db_temp.txt для перезаписи массива сотрудников
            //            {
            //                string line;

            //                while ((line = reader.ReadLine()) != null)
            //                {
            //                    if (String.Equals(line, temp) == true) // Если строка в файле совпадает с той, которую надо изменить
            //                    {
            //                        writer.WriteLine(note); // Записываем строку, с измененной информацией о сотруднике во временный файл
            //                    }
            //                    else // Если строки не совпают, то перезаписываем их во времененнй файл
            //                    {
            //                        writer.WriteLine(line);
            //                    }
            //                }
            //            }
            //        }
            //        File.Delete(this.path); // Удаляем исходный файл
            //        File.Move("db_temp.txt", this.path); // Переименовываем временный файл в исходный
            //        Console.WriteLine("Запись успешно изменена!");
            //    }
            //}
        }

        /// <summary>
        /// Метод Save(string Path) - перезаписывает измененные данные в файл
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
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
        /// Метод PrintDbToConsole() - выводит полную информацию о сотруднике на экран
        /// </summary>
        public void PrintDbToConsole(List<Employee> employeeList)
        {
            Console.WriteLine($"{"ID",4}\t{"Датa и время записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");

            foreach (var employee in employeeList)
            {
                Console.WriteLine(employee.Print());
            }
        }
        #endregion
    }
}