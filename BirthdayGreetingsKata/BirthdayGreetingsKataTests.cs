using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using Xunit;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsKataTests
    {
        [Fact]
        public void SendMail()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var smtpExePath = Path.Combine(baseDir, "support", "mailhog.exe");

            var proc = Process.Start(smtpExePath);

            using (var smtpClient = new SmtpClient("localhost", 1025))
            {
                smtpClient.Send("from@a.com", "to@a.com", "SendMail", "body");
            }

            proc.Kill();
            proc.Dispose();
        }

        [Fact]
        public void OneBithday()
        {
            
        }

    }
}
