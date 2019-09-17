using System;
using System.Diagnostics;
using System.IO;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    internal class LocalSmtpServer
    {
        private Process smtpServerProcess;
        private string smtpHost;
        private int smtpPort;
        private int apiPort;

        public LocalSmtpServer(string smtpHost, int smtpPort, int apiPort)
        {
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.apiPort = apiPort;
        }

        public void Start()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var smtpExePath = Path.Combine(baseDir, "support", "mailhog.exe");

            smtpServerProcess = Process.Start(smtpExePath, $"-hostname {smtpHost} -smtp-bind-addr :{smtpPort} -api-bind-addr :{apiPort} -ui-bind-addr :{apiPort}");
        }

        public void Stop()
        {
            smtpServerProcess.Kill();
            smtpServerProcess.Dispose();
        }
    }
}
