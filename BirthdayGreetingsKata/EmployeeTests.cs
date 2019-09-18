using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class EmployeeTests
    {
        [Fact]
        public void GoodDay()
        {
            DateTime dateOfBirth = new DateTime(1980, 2, 14);
            var employee = new Employee("", "", new BirthDate(dateOfBirth), "");

            Assert.True(employee.IsBirthday(new DateTime(2019, 2, 14)));
        }

        [Fact]
        public void BadDay()
        {
            DateTime dateOfBirth = new DateTime(1980, 2, 14);
            var employee = new Employee("", "", new BirthDate(dateOfBirth), "");

            Assert.False(employee.IsBirthday(new DateTime(2019, 5, 10)));
        }

    }
}
