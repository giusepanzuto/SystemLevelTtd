using System;

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

        public void SendGreetings(DateTime today)
        {
            var employees = employeeCsvCatalog.GetAll();

            foreach (var employee in employees)
            {
                if (employee.IsBirthday(today))
                {
                    smtpPostalOffice.SendMail(employee.Name, employee.Email);
                }
            }
        }
    }
}
