using System;
using System.Collections.Generic;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthDate : IEquatable<BirthDate>
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

        public override string ToString() => dateTime.ToString("yyyy-MM-dd");

        public override bool Equals(object obj)
        {
            return Equals(obj as BirthDate);
        }

        public bool Equals(BirthDate other)
        {
            return other != null &&
                   dateTime == other.dateTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(dateTime);
        }

        public bool IsBirthday(DateTime date)
        {
            return this.dateTime.Day == date.Day && this.dateTime.Month == date.Month;
        }

        public static bool operator ==(BirthDate left, BirthDate right)
        {
            return EqualityComparer<BirthDate>.Default.Equals(left, right);
        }

        public static bool operator !=(BirthDate left, BirthDate right)
        {
            return !(left == right);
        }
    }
}
