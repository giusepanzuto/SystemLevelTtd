using System;
using System.IO;
using System.Linq;
using SystemLevelTtd.BirthdayGreetingsKata.Adapters;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class EmployeeCsvCatalogTests
    {
        private const string filename = "EmployeeCsvCatalogTests.csv";

        public EmployeeCsvCatalogTests()
        {
            File.Delete(filename);
        }

        [Fact]
        public void OneEmployee()
        {
            var employeeCsvCatalog = EmployeeCatalogWith(
                new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com"));

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(
                new[] { new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com") },
                employees);
        }

        [Fact]
        public void ManyEmployee()
        {
            var employeeCsvCatalog = EmployeeCatalogWith(
                new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com"),
                new Employee("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"),
                new Employee("John", "Wick", BirthDate.From("1987-09-11"), "john.wick@acme.com")
            );

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(3, employees.Count);
        }

        [Fact]
        public void NoEmployee()
        {
            PrepareEmployeeFile();
            var employeeCsvCatalog = new EmployeeCsvCatalog(filename);

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(
                Enumerable.Empty<Employee>(),
                employees);
        }

        [Fact]
        public void MissingFile()
        {
            var employeeCsvCatalog = new EmployeeCsvCatalog(filename);
            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(
                Enumerable.Empty<Employee>(),
                employees);
        }

        private static EmployeeCsvCatalog EmployeeCatalogWith(params Employee[] employees)
        {
            PrepareEmployeeFile(employees.Select(ToCsv).ToArray());
            return new EmployeeCsvCatalog(filename);
        }

        private static string ToCsv(Employee employee)
        {
            return $"{employee.Surname},{employee.Name},{employee.DateOfBirth},{employee.Email}";
        }

        private static void PrepareEmployeeFile(params string[] contents)
        {
            const string header = "last_name, first_name, date_of_birth, email";
            File.WriteAllLines(filename, new[] { header }.Concat(contents));
        }
    }
}
