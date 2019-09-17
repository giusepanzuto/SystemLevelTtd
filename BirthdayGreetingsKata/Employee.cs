using System;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    internal class Employee
    {
        public Employee(string name, string surname, DateTime dateOfBirth, string email)
        {
            this.Name = name;
            this.Surname = surname;
            this.DateOfBirth = dateOfBirth;
            this.Email = email;
        }

        public string Name { get; }
        public string Surname { get; }
        public DateTime DateOfBirth { get; }
        public string Email { get; }

        public bool IsBirthday(DateTime date)
        {
            return this.DateOfBirth.Day == date.Day && this.DateOfBirth.Month == date.Month;
        }
    }
}
