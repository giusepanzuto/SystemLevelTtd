using System;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsService
    {
        private readonly string from;
        private readonly string employeesFilename;
        private readonly string smtpHost;
        private readonly int smtpPort;

        public BirthdayGreetingsService(string employeesFilename, string smtpHost, int smtpPort, string from)
        {
            this.employeesFilename = employeesFilename;
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.from = from;
        }

        public void SendGreetings(DateTime today)
        {
            var allLines = File.ReadAllLines(employeesFilename).Skip(1).ToList();

            foreach (var employee in allLines)
            {
                var employeeParts = employee.Split(',').Select(v => v.Trim()).ToList();
                var birthday = DateTime.Parse(employeeParts[2]);

                if (IsBirthday(today, birthday))
                {
                    var to = employeeParts[3];
                    var subject = "Happy Birthday!";
                    var name = employeeParts[1];
                    var body = $"Happy Birthday, dear {name}!";

                    using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                        smtpClient.Send(from, to, subject, body);
                }
            }
        }

        private static bool IsBirthday(DateTime today, DateTime birthday)
        {
            return birthday.Day == today.Day && birthday.Month == today.Month;
        }
    }
}
