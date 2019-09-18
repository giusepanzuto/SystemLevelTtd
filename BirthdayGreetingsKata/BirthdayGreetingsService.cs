using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemLevelTtd.BirthdayGreetingsKata
{
    public class BirthdayGreetingsService
    {
        private readonly IPostalOffice _postalOffice;
        private readonly IEmployeeCatalog _employeeCatalog;

        public BirthdayGreetingsService(IPostalOffice postalOffice, IEmployeeCatalog employeeCatalog)
        {
            _postalOffice = postalOffice;
            _employeeCatalog = employeeCatalog;
        }

        public void SendGreetings(DateTime today) => 
            _employeeCatalog.GetAll()
                .Where(e => e.IsBirthday(today))
                .ToList()
                .ForEach(e => _postalOffice.Send(e.Name, e.Email));
    }

    public interface IEmployeeCatalog
    {
        List<Employee> GetAll();
    }

    public interface IPostalOffice
    {
        void Send(string name, string to);
    }

}
