﻿using System.Net.Mail;
using SystemLevelTtd.BirthdayGreetingsKata.Adapters;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class SmtpPostalOfficeErrorTests
    {
        private const int SmtpPort = 1040;
        private const string SmtpHost = "localhost";
        private const string fromAddress = "greetings@acme.com";

        [Fact]
        public void SmtpDown()
        {
            var postalOffice = new SmtpPostalOffice(SmtpHost, SmtpPort, fromAddress);

            var ex = Record.Exception(() => postalOffice.Send("Pippo", "pippo@a.com"));

            Assert.IsType<SmtpException>(ex);
        }
    }
}
