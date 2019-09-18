using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class BirthdayGreetingsKataAcceptanceTests
    {
        [Fact]
        public void NoBithday()
        {
            var postalOffice = new PostalOfficeSpy();
            var employeeCatalog = new InMemoryEmployeeCatalog(
                new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com"),
                new Employee("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"),
                new Employee("John", "Wick", BirthDate.From("1987-09-11"), "john.wick@acme.com")
            );
            var service = new BirthdayGreetingsService(postalOffice, employeeCatalog);

            service.SendGreetings(new DateTime(2019, 2, 26));

            Assert.Empty(postalOffice.Sent);
        }

        [Fact]
        public void ManyBithdays()
        {
            var postalOffice = new PostalOfficeSpy();
            var employeeCatalog = new InMemoryEmployeeCatalog(
                new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com"),
                new Employee("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"),
                new Employee("John", "Wick", BirthDate.From("1987-09-11"), "john.wick@acme.com")
            );

            var service = new BirthdayGreetingsService(postalOffice, employeeCatalog);
            service.SendGreetings(new DateTime(2019, 9, 11));

            Assert.Equal(new List<(string name, string to)>
            {
                ("Pablo", "pablo.escobar@acme.com"),
                ("John", "john.wick@acme.com"),
            }, postalOffice.Sent);
        }
    }

    public class PostalOfficeSpy : IPostalOffice
    {
        private readonly List<(string name, string to)> sent = new List<(string name, string to)>();

        public IList<(string name, string to)> Sent => sent.ToList();

        public void Send(string name, string to)
        {
            sent.Add((name, to));
        }
    }

    public class InMemoryEmployeeCatalog : IEmployeeCatalog
    {
        private readonly Employee[] employees;

        public InMemoryEmployeeCatalog(params Employee[] employees)
        {
            this.employees = employees;
        }

        public List<Employee> GetAll() => employees.ToList();
    }
}
