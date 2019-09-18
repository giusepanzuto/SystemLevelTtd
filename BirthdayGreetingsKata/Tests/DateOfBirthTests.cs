using System;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class DateOfBirthTests
    {
        [Fact]
        public void GoodDay()
        {
            var birthdate = BirthDate.From("1980-02-14");

            Assert.True(birthdate.IsBirthday(new DateTime(2019, 2, 14)));
        }

        [Fact]
        public void BadDay()
        {
            var birthdate = BirthDate.From("1980-02-14");

            Assert.False(birthdate.IsBirthday(new DateTime(2019, 5, 20)));
        }

    }
}
