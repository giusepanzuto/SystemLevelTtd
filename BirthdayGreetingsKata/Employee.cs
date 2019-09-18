using System;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    internal class Employee
    {
        public Employee(string name, string surname, BirthDate birthDate, string email)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = birthDate;
            Email = email;
        }

        public string Name { get; }
        public string Surname { get; }
        public BirthDate DateOfBirth { get; }
        public string Email { get; }

        public bool IsBirthday(DateTime date)
        {
            return this.DateOfBirth.IsBirthday(date);
        }
    }
}
