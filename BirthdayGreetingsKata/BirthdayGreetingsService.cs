using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsService
    {
        private readonly ISmtpPostalOffice smtpPostalOffice;
        private readonly IEmployeeCatalog _employeeCatalog;

        public BirthdayGreetingsService(ISmtpPostalOffice postalOffice, IEmployeeCatalog employeeCatalog)
        {
            smtpPostalOffice = postalOffice;
            _employeeCatalog = employeeCatalog;
        }

        public void SendGreetings(DateTime today) => 
            _employeeCatalog.GetAll()
                .Where(e => e.IsBirthday(today))
                .ToList()
                .ForEach(e => smtpPostalOffice.SendMail(e.Name, e.Email));
    }

    public interface IEmployeeCatalog
    {
        List<Employee> GetAll();
    }

    public interface ISmtpPostalOffice
    {
        void SendMail(string name, string to);
    }

}
