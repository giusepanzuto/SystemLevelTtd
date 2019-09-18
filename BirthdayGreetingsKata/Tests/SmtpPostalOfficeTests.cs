using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class SmtpPostalOfficeTests : IDisposable
    {
        private readonly LocalSmtpServer _smtpServer;
        private const int SmtpPort = 1040;
        private const int ApiPort = 1041;
        private const string SmtpHost = "localhost";
        private const string NL = "\r\n";
        private const string fromAddress = "greetings@acme.com";

        public SmtpPostalOfficeTests()
        {
            _smtpServer = new LocalSmtpServer(SmtpHost, SmtpPort, ApiPort);
            _smtpServer.Start();
        }

        public void Dispose()
        {
            _smtpServer?.Stop();
        }

        [Fact]
        public void ItWorks()
        {
            Assert.True(true);
        }
    }
}
