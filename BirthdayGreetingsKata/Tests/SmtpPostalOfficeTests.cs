﻿using System;
using System.Threading.Tasks;
using SystemLevelTtd.BirthdayGreetingsKata.Adapters;
using SystemLevelTtd.support;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class SmtpPostalOfficeTests : IDisposable
    {
        private readonly LocalSmtpServer _smtpServer;
        private const int SmtpPort = 1060;
        private const int ApiPort = 1061;
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
        public async Task OneMail()
        {
            var postalOffice = new SmtpPostalOffice(SmtpHost, SmtpPort, fromAddress);

            postalOffice.Send("Pippo", "pippo@a.com");

            var serverInfo = await _smtpServer.GetServerInfo();
            Assert.Equal(1, serverInfo.MailReceived);
            var msg = serverInfo.Messages[0];

            Assert.Equal(MailInfo(name: "Pippo", to: "pippo@a.com"), msg);
        }

        private static MailInfo MailInfo(string name, string to) =>
            new MailInfo(
                @from: fromAddress,
                to: to,
                subject: "Happy Birthday!",
                body: $"Happy Birthday, dear {name}!" + NL);
    }
}
