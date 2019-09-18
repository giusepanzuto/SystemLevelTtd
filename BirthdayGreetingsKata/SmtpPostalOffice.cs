using System.Net.Mail;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
   public class SmtpPostalOffice : IPostalOffice
    {
        private readonly string smtpHost;
        private readonly int smtpPort;
        private readonly string from;

        public SmtpPostalOffice(string smtpHost, int smtpPort, string from)
        {
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.from = from;
        }

        public void Send(string name, string to)
        {
            var subject = "Happy Birthday!";
            var body = $"Happy Birthday, dear {name}!";

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                smtpClient.Send(@from, to, subject, body);
        }
    }
}
