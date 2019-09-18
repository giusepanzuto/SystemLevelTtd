using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata.Tests
{
    public class MailInfoTests
    {
        [Fact]
        public void Equality()
        {
            Assert.Equal(
                new MailInfo("from@a.com", "to@b.com", "subject", "body"),
                new MailInfo("from@a.com", "to@b.com", "subject", "body")
            );
        }
    }
}
