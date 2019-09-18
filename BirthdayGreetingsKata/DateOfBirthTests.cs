using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
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

    public class BirthDate
    {
        private readonly DateTime dateTime;

        public BirthDate(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public static BirthDate From(string date)
        {
            return new BirthDate(DateTime.Parse(date));
        }

        public bool IsBirthday(DateTime date)
        {
            return this.dateTime.Day == date.Day && this.dateTime.Month == date.Month;
        }
    }
}
