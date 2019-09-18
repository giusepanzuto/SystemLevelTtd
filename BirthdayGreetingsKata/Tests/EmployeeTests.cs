using System;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void Equality()
        {
            Assert.Equal(
                new Employee("a", "b", BirthDate.From("1980-02-14"), "c"),
                new Employee("a", "b", BirthDate.From("1980-02-14"), "c")
            );
        }
    }
}
