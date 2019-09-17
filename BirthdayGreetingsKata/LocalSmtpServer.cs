using System;
using System.Diagnostics;
using System.IO;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    internal class LocalSmtpServer
    {
        private Process smtpServerProcess;

        public void StartSmtpServer()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var smtpExePath = Path.Combine(baseDir, "support", "mailhog.exe");

            smtpServerProcess = Process.Start(smtpExePath);
        }

        public void StopSmtpServer()
        {
            smtpServerProcess.Kill();
            smtpServerProcess.Dispose();
        }
    }
}
