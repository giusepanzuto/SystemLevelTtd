using System;
using System.Net.Mail;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsKataTests : IDisposable
    {
        private const int SmtpPort = 1030;
        private const int ApiPort = 1031;
        private const string SmtpHost = "localhost";
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
        public void SendMail()
        {
            using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort))
            {
                smtpClient.Send("from@a.com", "to@a.com", "SendMail", "body");
            }
        }

        [Fact]
        public void OneBithday()
        {
            
        }
    }
}
