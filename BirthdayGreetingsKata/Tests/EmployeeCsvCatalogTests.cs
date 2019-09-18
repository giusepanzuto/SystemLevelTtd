using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class EmployeeCsvCatalogTests
    {
        private const string employeesFilename = "EmployeeCsvCatalogTests.csv";

        [Fact]
        public void OneEmployee()
        {
            PrepareEmployeeFile(new[]
            {
                "Capone, Al, 1951-10-08, al.capone@acme.com",
            });

            var employeeCsvCatalog = new EmployeeCsvCatalog(employeesFilename);

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(
                new[] { new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com") },
                employees);
        }

        [Fact]
        public void ManyEmployee()
        {
            PrepareEmployeeFile(new[]
            {
                "Capone, Al, 1951-10-08, al.capone@acme.com",
                "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
                "Wick, John, 1987-02-11, john.wick@acme.com"
            });

            var employeeCsvCatalog = new EmployeeCsvCatalog(employeesFilename);

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(3, employees.Count);
        }

        private static void PrepareEmployeeFile(IEnumerable<string> contents)
        {
            const string header = "last_name, first_name, date_of_birth, email";
            File.WriteAllLines(employeesFilename, new[] { header }.Concat(contents));
        }
    }
}
