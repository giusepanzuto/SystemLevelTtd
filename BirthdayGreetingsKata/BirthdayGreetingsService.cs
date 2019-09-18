using System;
using System.Linq;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsService
    {
        private readonly SmtpPostalOffice smtpPostalOffice;
        private readonly EmployeeCsvCatalog employeeCsvCatalog;

        public BirthdayGreetingsService(SmtpPostalOffice postalOffice, EmployeeCsvCatalog employeeCatalog)
        {
            smtpPostalOffice = postalOffice;
            employeeCsvCatalog = employeeCatalog;
        }

        public void SendGreetings(DateTime today) => 
            employeeCsvCatalog.GetAll()
                .Where(e => e.IsBirthday(today))
                .ToList()
                .ForEach(e => smtpPostalOffice.SendMail(e.Name, e.Email));
    }
}
