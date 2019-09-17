using System;
using System.Net.Mail;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsKataTests : IDisposable
    {
        readonly LocalSmtpServer _smtpServer;

        public BirthdayGreetingsKataTests()
        {
            _smtpServer = new LocalSmtpServer();
            _smtpServer.StartSmtpServer();
        }

        public void Dispose()
        {
            _smtpServer.StopSmtpServer();
        }

        [Fact]
        public void SendMail()
        {
            using (var smtpClient = new SmtpClient("localhost", 1025))
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
