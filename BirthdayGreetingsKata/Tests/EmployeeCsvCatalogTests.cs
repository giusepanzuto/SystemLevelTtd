using System.IO;
using System.Linq;
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
            PrepareEmployeeFile("Capone, Al, 1951-10-08, al.capone@acme.com");

            var employeeCsvCatalog = new EmployeeCsvCatalog(filename);

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(
                new[] { new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com") },
                employees);
        }

        [Fact]
        public void ManyEmployee()
        {
            PrepareEmployeeFile(
                "Capone, Al, 1951-10-08, al.capone@acme.com",
                "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
                "Wick, John, 1987-02-11, john.wick@acme.com"
            );

            var employeeCsvCatalog = new EmployeeCsvCatalog(filename);

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
        public void EmptyEmployee()
        {
            File.WriteAllText(filename, string.Empty);

            var employeeCsvCatalog = new EmployeeCsvCatalog(filename);

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(
                Enumerable.Empty<Employee>(),
                employees);
        }

        [Fact]
        public void MissingEmployee()
        {
            var employeeCsvCatalog = new EmployeeCsvCatalog(filename);

            var employees = employeeCsvCatalog.GetAll();

            Assert.Equal(
                Enumerable.Empty<Employee>(),
                employees);
        }


        private static void PrepareEmployeeFile(params string[] contents)
        {
            const string header = "last_name, first_name, date_of_birth, email";
            File.WriteAllLines(filename, new[] { header }.Concat(contents));
        }
    }
}
