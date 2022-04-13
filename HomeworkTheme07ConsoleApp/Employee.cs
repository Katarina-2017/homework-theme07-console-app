using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTheme07ConsoleApp
{
    /// <summary>
    /// Структура "Сотрудник"
    /// </summary>
    struct Employee 
    {
        #region Поля
        /// <summary>
        /// Поле "ID"
        /// </summary>
        private int id;

        /// <summary>
        /// Поле "Дата и время добавления записи"
        /// </summary>
        private DateTime recordCreationDate;

        /// <summary>
        /// Поле "Ф.И.О."
        /// </summary>
        private string initialsEmployee;

        /// <summary>
        /// Поле "Возраст"
        /// </summary>
        private byte age;

        /// <summary>
        /// Поле "Рост"
        /// </summary>
        private int height;

        /// <summary>
        /// Поле "Дата рождения"
        /// </summary>
        private DateTime dateOfBirth;

        /// <summary>
        /// Поле "Место рождения"
        /// </summary>
        private string birthPlace;

        #endregion

        #region Свойства
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// Дата и время добавления записи
        /// </summary>
        public DateTime RecordCreationDate { get { return this.recordCreationDate; } set { this.recordCreationDate = value; } }

        /// <summary>
        /// Ф.И.О.
        /// </summary>
        public string InitialsEmployee { get { return this.initialsEmployee; } set { this.initialsEmployee = value; } }

        /// <summary>
        /// Возраст
        /// </summary>
        public byte Age { get { return this.age; } set { this.age = value; } }

        /// <summary>
        /// Рост
        /// </summary>
        public int Height { get { return this.height; } set { this.height = value; } }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get { return this.dateOfBirth.Date; } set { this.dateOfBirth = value; } }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string BirthPlace { get { return this.birthPlace; } set { this.birthPlace = value; } }

        #endregion

        #region Конструкторы
        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="Id">Номер записи</param>
        /// <param name="RecordCreationDate">Дата и время добавления записи</param>
        /// <param name="InitialsEmployee">Ф.И.О.</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Height">Рост</param>
        /// <param name="DateOfBirth">Дата рождения</param>
        /// <param name="BirthPlace">Место рождения</param>
        public Employee(int Id, DateTime RecordCreationDate, string InitialsEmployee, byte Age, int Height,
            DateTime DateOfBirth, string BirthPlace)
        {
            this.id = Id;
            this.recordCreationDate = RecordCreationDate;
            this.initialsEmployee = InitialsEmployee;
            this.age = Age;
            this.height = Height;
            this.dateOfBirth = DateOfBirth;
            this.birthPlace = BirthPlace;

        }

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="Id">Номер записи</param>
        /// <param name="InitialsEmployee">Ф.И.О.</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Height">Рост</param>
        /// <param name="DateOfBirth">Дата рождения</param>
        /// <param name="BirthPlace">Место рождения</param>
        public Employee(int Id, string InitialsEmployee, byte Age, int Height,
            DateTime DateOfBirth, string BirthPlace) :
            this(Id, DateTime.Now, InitialsEmployee, Age, Height, DateOfBirth, BirthPlace)
        {

        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод Print() - печать полной информации о сотруднике
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{Id,4} {RecordCreationDate,30} {InitialsEmployee,29} {Age,10} {Height,9} {DateOfBirth.ToShortDateString(),15} {BirthPlace,25}";
        }

        #endregion
    }
}
