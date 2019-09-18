using System;
using System.IO;
using System.Linq;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsService
    {
        private readonly string from;
        private readonly SmtpPostalOffice smtpPostalOffice;
        private readonly string employeesFilename;

        public BirthdayGreetingsService(string employeesFilename, string smtpHost, int smtpPort, string from)
        {
            this.employeesFilename = employeesFilename;
            this.from = from;

            this.smtpPostalOffice = new SmtpPostalOffice(smtpHost, smtpPort, from);
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
                    smtpPostalOffice.SendMail(employee.Name, employee.Email);
                }
            }
        }
    }
}
