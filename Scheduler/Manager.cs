using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Manager
    {
        private Lawyer Lawyer { get; set; } = new Lawyer();
        public void MakeSchedule()
        {
            int year = Lawyer.GetYear("What year is this schedule being made?");
            int month = Lawyer.GetMonth("What month of the year: " + year + " do you want to make the schedule for?");
            DateTime date = new DateTime(year, month, 1);
            for(int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {

            }
        }
    }
}
