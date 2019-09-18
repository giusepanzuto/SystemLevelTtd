using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class BirthdayGreetingsKataTests : IDisposable
    {
        private const string employeesFilename = "test-data.csv";
        private const int SmtpPort = 1030;
        private const int ApiPort = 1031;
        private const string SmtpHost = "localhost";
        private const string NL = "\r\n";
        private const string fromAddress = "greetings@acme.com";
        private LocalSmtpServer _smtpServer;

        public BirthdayGreetingsKataTests()
        {
            _smtpServer = new LocalSmtpServer(SmtpHost, SmtpPort, ApiPort);
            _smtpServer.Start();
        }

        public void Dispose()
        {
            _smtpServer?.Stop();
        }

        [Fact]
        public async Task SendMail()
        {
            using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort)) 
                smtpClient.Send("from@a.com", "to@a.com", "SendMail", "body");

            var receivedEmails = await _smtpServer.GetServerInfo();

            Assert.Equal(1, receivedEmails.MailReceived);
        }

        [Fact]
        public async Task NoBithday()
        {
            PrepareEmployeeFile(new[]
            {
                "Capone, Al, 1951-10-08, al.capone@acme.com",
                "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
                "Wick, John, 1987-09-11, john.wick@acme.com"
            });

            var service = new BirthdayGreetingsService(new SmtpPostalOffice(SmtpHost, SmtpPort, fromAddress), new EmployeeCsvCatalog(employeesFilename));
            service.SendGreetings(new DateTime(2019, 2, 26));

            var serverInfo = await _smtpServer.GetServerInfo();

            Assert.Equal(0, serverInfo.MailReceived);
        }

        [Fact]
        public async Task ManyBithdays()
        {
            PrepareEmployeeFile(new[]
            {
                "Capone, Al, 1951-10-08, al.capone@acme.com",
                "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
                "Wick, John, 1987-09-11, john.wick@acme.com"
            });

            var service = new BirthdayGreetingsService(new SmtpPostalOffice(SmtpHost, SmtpPort, fromAddress), new EmployeeCsvCatalog(employeesFilename));
            service.SendGreetings(new DateTime(2019, 9, 11));

            var serverInfo = await _smtpServer.GetServerInfo();

            Assert.Equal(2, serverInfo.MailReceived);
        }

        private static void PrepareEmployeeFile(IEnumerable<string> contents)
        {
            const string header = "last_name, first_name, date_of_birth, email";
            File.WriteAllLines(employeesFilename, new[] { header }.Concat(contents));
        }
    }
}
