using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class SmtpServerErrorTests
    {
        private const string employeesFilename = "test-data-2.csv";
        private const int SmtpPort = 1035;
        private const string SmtpHost = "localhost";
        private const string fromAddress = "greetings@acme.com";

        [Fact]
        public void SmtpDown()
        {
            PrepareEmployeeFile(new[]
            {
                "Capone, Al, 1951-10-08, al.capone@acme.com",
                "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
                "Wick, John, 1987-09-11, john.wick@acme.com"
            });

            var service = new BirthdayGreetingsService(employeesFilename, SmtpHost, SmtpPort, fromAddress);
            var ex = Record.Exception(() => service.SendGreetings(new DateTime(2019, 9, 11)));

            Assert.IsType<SmtpException>(ex);
        }
        private static void PrepareEmployeeFile(IEnumerable<string> contents)
        {
            const string header = "last_name, first_name, date_of_birth, email";
            File.WriteAllLines(employeesFilename, new[] { header }.Concat(contents));
        }
    }
}
