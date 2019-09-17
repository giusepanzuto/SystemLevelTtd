using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsKataTests : IDisposable
    {
        private const int SmtpPort = 1030;
        private const int ApiPort = 1031;
        private const string SmtpHost = "localhost";
        private const string NL = "\r\n";
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
            File.WriteAllLines("test-data.csv", new []
            {
                "last_name, first_name, date_of_birth, email",
                "Capone, Al, 1951-10-08, al.capone@acme.com",
                "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
                "Wick, John, 1987-09-11, john.wick@acme.com"
            });

            var service = new BirthdayGreetingsService(SmtpHost, SmtpPort, "greetings@acme.com");
            service.SendGreetings(new DateTime(2019, 9, 11));

            var serverInfo = await _smtpServer.GetServerInfo();

            Assert.Equal(1, serverInfo.MailReceived);

            var msg = serverInfo.Messages[0];
            Assert.Equal("greetings@acme.com", msg.From);
            Assert.Equal("pablo.escobar@acme.com", msg.To);
            Assert.Equal("Happy Birthday!", msg.Subject);
            Assert.Equal("Happy Birthday, dear Pablo!"+ NL, msg.Body);
        }
    }

    public class BirthdayGreetingsService
    {
        private readonly string from;
        private readonly string smtpHost;
        private readonly int smtpPort;

        public BirthdayGreetingsService(string smtpHost, int smtpPort, string @from)
        {
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.from = from;
        }

        public void SendGreetings(DateTime today)
        {
            var to = "pablo.escobar@acme.com";
            var subject = "Happy Birthday!";
            var body = "Happy Birthday, dear Pablo!";

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                smtpClient.Send(from, to, subject, body);
        }
    }
}
