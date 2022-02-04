using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTheme07ConsoleApp
{
    struct Employee
    {
        #region Поля

        private int id;

        private DateTime recordCreationDate;

        private DateTime recordCreationTime;

        private string lastName;

        private string firstName;

        private string patronymic;

        private byte age;

        private int height;

        private DateTime dateOfBirth;

        private string birthPlace;

        #endregion

        #region Свойства

        public int Id { get { return this.id; } set { this.id = value; } }

        public DateTime RecordCreationDate { get { return this.recordCreationDate; } set { this.recordCreationDate = value; } }

        public DateTime RecordCreationTime { get { return this.recordCreationTime; } set { this.recordCreationTime = value; } }

        public string LastName { get { return this.lastName; } set { this.lastName = value; } }

        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        public string Patronymic { get { return this.patronymic; } set { this.patronymic = value; } }

        public byte Age { get { return this.age; } set { this.age = value; } }

        public int Height { get { return this.height; } set { this.height = value; } }

        public DateTime DateOfBirth { get { return this.dateOfBirth; } set { this.dateOfBirth = value; } }

        public string BirthPlace { get { return this.birthPlace; } set { this.birthPlace = value; } }

        #endregion

        #region Конструкторы

        public Employee(int Id, DateTime RecordCreationDate, DateTime RecordCreationTime, string LastName, string FirstName, string Patronymic, byte Age, int Height,
            DateTime DateOfBirth, string BirthPlace)
        {
            this.id = Id;
            this.recordCreationDate = RecordCreationDate;
            this.recordCreationTime = RecordCreationTime;
            this.lastName = LastName;
            this.firstName = FirstName;
            this.patronymic = Patronymic;
            this.age = Age;
            this.height = Height;
            this.dateOfBirth = DateOfBirth;
            this.birthPlace = BirthPlace;

        }

        #endregion
    }
}
