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

            foreach (var employeeLine in allLines)
            {
                var employeeParts = employeeLine.Split(',').Select(v => v.Trim()).ToList();
                var dateOfBirth = DateTime.Parse(employeeParts[2]);
                var name = employeeParts[1];
                var to = employeeParts[3];

                var employee = new Employee(name: employeeParts[1], surname: employeeParts[0], birthDate: new BirthDate(dateOfBirth), email: employeeParts[3]);

                if (employee.IsBirthday(today))
                {
                    var subject = "Happy Birthday!";
                    var body = $"Happy Birthday, dear {name}!";

                    using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                        smtpClient.Send(from, to, subject, body);
                }
            }
        }
    }
}
