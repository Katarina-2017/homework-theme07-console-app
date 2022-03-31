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
        private Employee[] employees; // Основной массив для хранения данных

        private string path; // Путь к файлу с данными

        private int idRecord; // Идентификатор записи

        int index; // Текущий элемент для добавления в employees

        #region Конструкторы
        /// <summary>
        /// Создание репозитория
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными

            this.index = 0; // Текущая позиция для добавления сотрудника в  employees 

            this.idRecord = 0; // Текущий индентификатор записи

            this.employees = new Employee[1]; // Инициализация массива сотрудников.    | изначально предпологаем, что данных нет

            this.GetAll(); // Получаем всех сотрудников
        }

        /// <summary>
        /// Создание репозитория
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
        /// <param name="IdRecord">Идентификатор записи</param>
        public Repository(string Path, int IdRecord)
        {
            this.path = Path;
            
            this.index = 0;

            this.idRecord = IdRecord;

            this.employees = new Employee[1];

            this.GetById(IdRecord); // Получаем сотрудников с заданным ID
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

            this.index = 0;

            this.idRecord = 0;

            this.employees = new Employee[1];

            this.GetAll(); // Получаем всех сотрудников

            if (dateStart < dateEnd) // Начальная дата должна быть меньше конечной даты диапазона
            {
                Console.WriteLine($"{"ID",4}\t{"Датa и время записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");

                for (int i = 0; i < index; i++)
                {
                    if (this.employees[i].RecordCreationDate >= dateStart && this.employees[i].RecordCreationDate <= dateEnd) // Если дата создания записи попадает в заданный диапазон
                    {
                        Console.WriteLine(this.employees[i].Print()); // Выводим массив сотрудников на экран
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

            this.index = 0;

            this.idRecord = 0;

            this.employees = new Employee[1];

            this.GetAll(); // Получаем всех сотрудников

            switch (userWay)
            {
                case 1:
                    // сортировка пузырьком
                    Employee tmp;

                    for (int i = index - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            // сравниваем элементы массива сотрудников по дате и времени создания записи
                            if (employees[j].RecordCreationDate > employees[j + 1].RecordCreationDate)
                            {
                                tmp = employees[j];
                                employees[j] = employees[j + 1];
                                employees[j + 1] = tmp;
                            }
                        }
                    }
                    break;
                case 2:
                    // сортировка пузырьком
                    Employee itm;

                    for (int i = index - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            // сравниваем элементы массива сотрудников по дате и времени создания записи
                            if (employees[j].RecordCreationDate < employees[j + 1].RecordCreationDate)
                            {
                                itm = employees[j];
                                employees[j] = employees[j + 1];
                                employees[j + 1] = itm;
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение");
                    break;
            }
        }
        #endregion

        #region Методы
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

        /// <summary>
        /// Метод GetAll() - получает всех сотрудников
        /// </summary>
        private void GetAll()
        {
            FileInfo userFileName = new FileInfo(this.path);

            if (userFileName.Exists) // Если файл существует, то считываем из него информацию
            {
                using (StreamReader sr = new StreamReader(this.path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split('#');

                        if (args[0] !="") // Если строка не пустая, добавляем сотрудника в массив-хранилище
                        {
                            Add(new Employee(Convert.ToInt32(args[0]), DateTime.ParseExact(args[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture), args[2], Convert.ToByte(args[3]), Convert.ToInt32(args[4]),
                            DateTime.ParseExact(args[5], "dd.MM.yyyy", CultureInfo.InvariantCulture), args[6]));
                        }
                        else // Иначе выводим соотвествующее сообщение
                        {
                            Console.WriteLine($"Файл {this.path} пуст");
                        }
                    }
                }
            }
            else // Иначе выводим соответсвующее сообщение 
            {
                Console.WriteLine($"Файл с именем {this.path} не найден.");
            }
        }

        /// <summary>
        /// Метод GetById(int idRecord) - получает сотрудника по идентификатору
        /// </summary>
        /// <param name="idRecord">Идентификатор записи</param>
        private void GetById(int idRecord)
        {
            FileInfo userFileName = new FileInfo(this.path);
            if (userFileName.Exists) // Если файл существует, то считываем из него информацию
            {
                using (StreamReader sr = new StreamReader(this.path))
                {
                    bool check = false; // Переменная для проверки на наличие записи с заданным идентификатором

                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split('#');

                        if (args[0] =="")
                        {
                            Console.WriteLine($"Файл {this.path} пуст");
                        }
                        else
                        {
                            if (idRecord == Convert.ToInt32(args[0])) // Если введенный номер совпадает с идентификатором записи в файле, то добавляем этого сотрудника в массив-хранилище
                            {
                                Add(new Employee(Convert.ToInt32(args[0]), DateTime.ParseExact(args[1], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture), args[2], Convert.ToByte(args[3]), Convert.ToInt32(args[4]),
                                DateTime.ParseExact(args[5], "dd.MM.yyyy", CultureInfo.InvariantCulture), args[6]));
                                check = true;
                            }
                        }
                    }
                    if (check == false) // Если записи с заданным идентификатором нет в файле, то выводим соотвествующее сообщение
                    {
                        Console.WriteLine($"Записи с таким номером {this.idRecord} не найдено");
                    }
                }
            }
            else // Иначе выводим соответсвующее сообщение 
            {
                Console.WriteLine($"Файл с именем {this.path} не найден.");
            }
        }

        /// <summary>
        /// Метод Create() - создает нового сотрудника
        /// </summary>
        public void Create()
        {
            char key = 'д';
            int noteId = this.index; // Сохраняем количество элементов массива-хранилища
            
            do
            {
                noteId++; // Определяем ID новой записи автоматически, исходя из количества элементов в массиве
                Console.WriteLine($"\nID записи: {noteId}");

                string nowDate = DateTime.Now.ToShortDateString();
                string nowTime = DateTime.Now.ToShortTimeString();
                string noteDate = nowDate + " " + nowTime;
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

                Add(new Employee(noteId, DateTime.ParseExact(noteDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture), noteInitials, noteAge, noteHeight,
                    DateTime.ParseExact(noteDateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture), noteBirthPlace)); // Добавляем сотрудника в массив
                Console.WriteLine("Продолжить н/д"); key = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(key) == 'н');

            PrintDbToConsole(); // Выводим массив сотрудников на экран
        }

        /// <summary>
        /// Метод Delete(Repository employees) - удаляет заданного сотрудника
        /// </summary>
        /// <param name="employees">Массив сотрудников для удаления</param>
        public void Delete(Repository employees)
        {
            string temp = "";
            
            for (int i = 0; i < this.index; i++)
            {
                bool check = false; // Переменная для проверки удаления записи
                temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", // Формируем временную строку в определенном формате из конкретного сотрудника, которого необходимо удалить
                                        this.employees[i].Id,
                                        this.employees[i].RecordCreationDate,
                                        this.employees[i].InitialsEmployee,
                                        this.employees[i].Age,
                                        this.employees[i].Height,
                                        this.employees[i].DateOfBirth,
                                        this.employees[i].BirthPlace);

                if (temp != "") // Дополнительная проверка на пустоту строки
                {
                    Console.WriteLine("Вы выбрали эту запись для удаления:");
                    Console.WriteLine(temp);

                    using (StreamReader reader = new StreamReader(this.path))
                    {
                        using (StreamWriter writer = new StreamWriter(@"db_temp.txt")) // Создаем временный файл db_temp.txt для перезаписи массива сотрудников
                        {
                            string line;

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (String.Equals(line, temp) == false) // Если строка в файле не совпадает с той, которую надо удалить
                                {
                                    writer.WriteLine(line); // Записываем эту строку во временный файл, т.е. формируем новый файл без строк, которые надо удалить
                                }
                                else // Если строки совпадают оставляем ее в исходном файле, check присваиваем истина
                                {
                                    check = true;
                                }
                            }
                        }
                    }

                    File.Delete(this.path); // Удаляем исходный файл
                    File.Move("db_temp.txt", this.path); // Переименовываем временный файл в исходный 

                    if (check == true) // Если удаление прошло успешно, то выводим соответствующее сообщение
                    {
                        Console.WriteLine("Запись успешно удалена!");
                    }
                    else // Если строки не совпали и удаление не прошло, выводим соответствующее сообщение
                    {
                        Console.WriteLine("Что-то пошло не так...Запись не удалена!");
                    }
                }
            }
        }

        /// <summary>
        /// Метод Update(Repository employees) - редактирует заданного сотрудника
        /// </summary>
        /// <param name="employees">Массив сотрудников для редактирования</param>
        public void Update(Repository employees)
        {
            string temp = "";
            int noteId = 0;

            for (int i = 0; i < this.index; i++)
            {
                temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", // Формируем временную строку в определенном формате из конкретного сотрудника, которого необходимо изменить
                                        this.employees[i].Id,
                                        this.employees[i].RecordCreationDate,
                                        this.employees[i].InitialsEmployee,
                                        this.employees[i].Age,
                                        this.employees[i].Height,
                                        this.employees[i].DateOfBirth,
                                        this.employees[i].BirthPlace);
                noteId = this.employees[i].Id; // Сохраняем идентификатор этой записи

                if (temp != "") // // Дополнительная проверка на пустоту строки
                {
                    Console.WriteLine("Вы выбрали эту запись для редактирования:");
                    Console.WriteLine(temp);

                    string note = string.Empty;

                    Console.WriteLine("Введите новые значения полей:");
                    Console.WriteLine($"\nID записи: {noteId}"); // Идентификатор записи не изменяем, всю остальную информацию редактируем свободно
                    note += $"{noteId}" + "#";

                    Console.WriteLine($"Дата и время добавления записи:");
                    note += $"{Console.ReadLine()}" + "#";

                    Console.WriteLine("Ф. И. О.: ");
                    note += $"{Console.ReadLine()}" + "#";

                    Console.WriteLine("Возраст: ");
                    note += $"{Console.ReadLine()}" + "#";

                    Console.WriteLine("Рост: ");
                    note += $"{Console.ReadLine()}" + "#";

                    Console.WriteLine("Датa рождения: ");
                    note += $"{Console.ReadLine()}" + " 00:00" + "#";

                    Console.WriteLine("Место рождения: ");
                    note += $"{Console.ReadLine()}";

                    using (StreamReader reader = new StreamReader(this.path))
                    {
                        using (StreamWriter writer = new StreamWriter(@"db_temp.txt")) // Создаем временный файл db_temp.txt для перезаписи массива сотрудников
                        {
                            string line;

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (String.Equals(line, temp) == true) // Если строка в файле совпадает с той, которую надо изменить
                                {
                                    writer.WriteLine(note); // Записываем строку, с измененной информацией о сотруднике во временный файл
                                }
                                else // Если строки не совпают, то перезаписываем их во времененнй файл
                                {
                                    writer.WriteLine(line);
                                }
                            }
                        }
                    }
                    File.Delete(this.path); // Удаляем исходный файл
                    File.Move("db_temp.txt", this.path); // Переименовываем временный файл в исходный
                    Console.WriteLine("Запись успешно изменена!");
                }
            }
        }

        /// <summary>
        /// Метод Save(string Path) - перезаписывает измененные данные в файл
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
        public void Save(string Path)
        {
            FileInfo userFileName = new FileInfo(path);

            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < this.index; i++)
                {
                    string temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", // Формирует строки в определенном формате через #
                                            this.employees[i].Id,
                                            this.employees[i].RecordCreationDate,
                                            this.employees[i].InitialsEmployee,
                                            this.employees[i].Age,
                                            this.employees[i].Height,
                                            this.employees[i].DateOfBirth,
                                            this.employees[i].BirthPlace);
                    sw.WriteLine($"{temp}"); // Записываем эту строку в исходный файл
                }
            }
        }

        /// <summary>
        /// Метод PrintDbToConsole() - выводит полную информацию о сотруднике на экран
        /// </summary>
        public void PrintDbToConsole()
        {
            Console.WriteLine($"{"ID",4}\t{"Датa и время записи",5}\t{" Ф.И.О.",25}\t{"Возраст",4}\t{"Рост",7}\t{"Датa рождения",15}\t{" Место рождения",25}");

            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.employees[i].Print());
            }
        }
        #endregion
    }
}