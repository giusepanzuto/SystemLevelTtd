using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class EmployeeCsvCatalog
    {
        private string employeesFilename;

        public EmployeeCsvCatalog(string employeesFilename)
        {
            this.employeesFilename = employeesFilename;
        }

        private Employee ParseEmployee(string employeeLine)
        {
            var employeeParts = employeeLine.Split(',').Select(v => v.Trim()).ToList();
            var dateOfBirth = DateTime.Parse(employeeParts[2]);
            var name = employeeParts[1];
            var to = employeeParts[3];

            var employee = new Employee(name: employeeParts[1], surname: employeeParts[0], birthDate: new BirthDate(dateOfBirth), email: employeeParts[3]);
            return employee;
        }

        public List<Employee> GetAll()
        {
            var allLines = File.ReadAllLines(employeesFilename).Skip(1).ToList();

            var employees = File.ReadAllLines(employeesFilename)
                .Skip(1)
                .Select(employeeLine => ParseEmployee(employeeLine))
                .ToList();

            return employees;
        }
    }
}
