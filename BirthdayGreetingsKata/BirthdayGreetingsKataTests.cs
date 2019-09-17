using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsKataTests : IDisposable
    {
        private const string employeesFilename = "test-data.csv";
        private const int SmtpPort = 1030;
        private const int ApiPort = 1031;
        private const string SmtpHost = "localhost";
        private const string NL = "\r\n";
        private const string fromAddress = "greetings@acme.com";
        readonly LocalSmtpServer _smtpServer;

        public BirthdayGreetingsKataTests()
        {
            _smtpServer = new LocalSmtpServer(SmtpHost, SmtpPort, ApiPort);
            _smtpServer.Start();
        }

        public void Dispose()
        {
            _smtpServer.Stop();
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
        public async Task OneBithday()
        {
            PrepareEmployeeFile(new[]
            {
                "Capone, Al, 1951-10-08, al.capone@acme.com",
                "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
                "Wick, John, 1987-02-11, john.wick@acme.com"
            });

            var service = new BirthdayGreetingsService(employeesFilename, SmtpHost, SmtpPort, fromAddress);
            service.SendGreetings(new DateTime(2019, 9, 11));

            var serverInfo = await _smtpServer.GetServerInfo();

            Assert.Equal(1, serverInfo.MailReceived);

            var msg = serverInfo.Messages[0];
            Assert.Equal(fromAddress, msg.From);
            Assert.Equal("pablo.escobar@acme.com", msg.To);
            Assert.Equal("Happy Birthday!", msg.Subject);
            Assert.Equal("Happy Birthday, dear Pablo!" + NL, msg.Body);
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

            var service = new BirthdayGreetingsService(employeesFilename, SmtpHost, SmtpPort, fromAddress);
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

            var service = new BirthdayGreetingsService(employeesFilename, SmtpHost, SmtpPort, fromAddress);
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

                if (birthday.Day == today.Day && birthday.Month == today.Month)
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
    }
}
