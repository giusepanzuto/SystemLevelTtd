using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        private static void PrepareEmployeeFile(IEnumerable<string> contents)
        {
            const string header = "last_name, first_name, date_of_birth, email";
            File.WriteAllLines(employeesFilename, new[] { header }.Concat(contents));
        }
    }
}
