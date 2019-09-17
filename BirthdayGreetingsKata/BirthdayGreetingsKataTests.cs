using System.Net.Mail;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsKataTests
    {
        [Fact]
        public void ItWorks()
        {
            Assert.True(true);
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
