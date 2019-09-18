using System;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
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
