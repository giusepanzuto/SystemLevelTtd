using System;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsService
    {
        private readonly string from;
        private readonly SmtpPostalOffice smtpPostalOffice;
        private readonly string employeesFilename;
        private readonly string smtpHost;
        private readonly int smtpPort;

        public BirthdayGreetingsService(string employeesFilename, string smtpHost, int smtpPort, string from)
        {
            this.employeesFilename = employeesFilename;
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.from = from;

            this.smtpPostalOffice = new SmtpPostalOffice();
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
                    smtpPostalOffice.SendMail(name, to, smtpHost, smtpPort, from);
                }
            }
        }
    }

    public class SmtpPostalOffice
    {
        public SmtpPostalOffice()
        {
        }

        public void SendMail(string name, string to, string smtpHost, int smtpPort, string from)
        {
            var subject = "Happy Birthday!";
            var body = $"Happy Birthday, dear {name}!";

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                smtpClient.Send(@from, to, subject, body);
        }
    }
}
