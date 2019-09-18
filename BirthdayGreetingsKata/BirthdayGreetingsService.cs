using System;
using System.Linq;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsService
    {
        private readonly SmtpPostalOffice smtpPostalOffice;
        private readonly EmployeeCsvCatalog employeeCsvCatalog;

        public BirthdayGreetingsService(string employeesFilename, string smtpHost, int smtpPort, string from)
        {
            smtpPostalOffice = new SmtpPostalOffice(smtpHost, smtpPort, from);
            employeeCsvCatalog = new EmployeeCsvCatalog(employeesFilename);
        }

        public void SendGreetings(DateTime today) => 
            employeeCsvCatalog.GetAll()
                .Where(e => e.IsBirthday(today))
                .ToList()
                .ForEach(e => smtpPostalOffice.SendMail(e.Name, e.Email));
    }
}
