using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTheme07ConsoleApp
{
    struct Employee 
    {
        #region Поля

        private int id;

        private DateTime recordCreationDate;

        private string initialsEmployee;

        private byte age;

        private int height;

        private DateTime dateOfBirth;

        private string birthPlace;

        #endregion

        #region Свойства

        public int Id { get { return this.id; } set { this.id = value; } }

        public DateTime RecordCreationDate { get { return this.recordCreationDate; } set { this.recordCreationDate = value; } }

        public string InitialsEmployee { get { return this.initialsEmployee; } set { this.initialsEmployee = value; } }

        public byte Age { get { return this.age; } set { this.age = value; } }

        public int Height { get { return this.height; } set { this.height = value; } }

        public DateTime DateOfBirth { get { return this.dateOfBirth.Date; } set { this.dateOfBirth = value; } }

        public string BirthPlace { get { return this.birthPlace; } set { this.birthPlace = value; } }

        #endregion

        #region Конструкторы

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

        public Employee(int Id, string InitialsEmployee, byte Age, int Height,
            DateTime DateOfBirth, string BirthPlace) :
            this(Id, DateTime.Now, InitialsEmployee, Age, Height, DateOfBirth, BirthPlace)
        {
            
        }
        #endregion

        #region Методы

        public string Print()
        {
            string createDate = RecordCreationDate.ToShortDateString() + " " + RecordCreationDate.ToShortTimeString();
            
            return $"{Id,4} {createDate,22} {InitialsEmployee,29} {Age,10} {Height,9} {DateOfBirth.ToShortDateString(),15} {BirthPlace,25}";
        }

        #endregion
    }
}
