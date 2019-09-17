using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<ServerInfo> GetServerInfo()
        {
            string httpAddress = $"http://{smtpHost}:{apiPort}/api/v2/messages";
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(httpAddress);

            dynamic mails = JObject.Parse(response);
            int count = mails.count;
            var messages = new List<MailInfo>();
            for (int i = 0; i < count; i++)
            {
                dynamic msg = mails.items[i];
                string from = msg.From.Mailbox + "@" + msg.From.Domain;
                string to = msg.To[0].Mailbox + "@" + msg.To[0].Domain;
                string subject = msg.Content.Headers.Subject[0];
                string body = msg.Content.Body;
                messages.Add(new MailInfo { From = @from, To = to, Subject = subject, Body = body });
            }

            return new ServerInfo
            {
                MailReceived = count,
                Messages = new List<MailInfo>(messages).ToArray()
            };
        }
        public class MailInfo
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }

        public class ServerInfo
        {
            public int MailReceived { get; set; }
            public MailInfo[] Messages { get; set; }
        }
    }
}
