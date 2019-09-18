using System;
using System.Collections.Generic;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class Employee : IEquatable<Employee>
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

        public override bool Equals(object obj)
        {
            return Equals(obj as Employee);
        }

        public bool Equals(Employee other)
        {
            return other != null &&
                   Name == other.Name &&
                   Surname == other.Surname &&
                   EqualityComparer<BirthDate>.Default.Equals(DateOfBirth, other.DateOfBirth) &&
                   Email == other.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, DateOfBirth, Email);
        }

        public bool IsBirthday(DateTime date)
        {
            return DateOfBirth.IsBirthday(date);
        }

        public static bool operator ==(Employee left, Employee right)
        {
            return EqualityComparer<Employee>.Default.Equals(left, right);
        }

        public static bool operator !=(Employee left, Employee right)
        {
            return !(left == right);
        }
    }
}
