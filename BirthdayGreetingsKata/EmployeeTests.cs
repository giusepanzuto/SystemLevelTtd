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
            var employee = new Employee("", "", new DateTime(1980, 2, 14), "");

            Assert.True(employee.IsBirthday(new DateTime(2019, 2, 14)));
        }

        [Fact]
        public void BadDay()
        {
            var employee = new Employee("", "", new DateTime(1980, 2, 14), "");

            Assert.False(employee.IsBirthday(new DateTime(2019, 5, 10)));
        }

    }
}
