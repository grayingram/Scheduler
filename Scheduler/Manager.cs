using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Manager
    {
        private Lawyer Lawyer { get; set; } = new Lawyer();
        private Reader Reader { get; set; } = new Reader();
        public void MakeSchedule()
        {
            int year = Lawyer.GetYear("What year is this schedule being made?");
            int month = Lawyer.GetMonth("What month of the year: " + year + " do you want to make the schedule for?");
            DateTime date = new DateTime(year, month, 1);
            for(int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {
                List<Employee> workable = Reader.GetWorkableEmployees(date);
                List<Employee> vacationing = Reader.GetVacationingEmployees(date);
                List<Employee> off = Reader.GetOffEmployees(date);
                List<Employee> sick = Reader.GetSickEmployees(date);
            }
        }
    }
}
