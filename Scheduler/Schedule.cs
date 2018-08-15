using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    class Schedule
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public int NumberOfEmployees { get; set; }
        public int DaysWorkable { get; set; }
        public Schedule()
        {
            Lawyer lawyer = new Lawyer();
            DaysWorkable = lawyer.GetInt("How many days of the week is this company open for?");
            NumberOfEmployees = lawyer.GetInt("How many employees work in this company?");
            for(int i = 0; i < NumberOfEmployees; i++)
            {
                Employee tempemployee = new Employee();
                Console.WriteLine("For Employee #:" + (i + 1));
                tempemployee.Name = lawyer.GetResponse("What is the Name of this employee?");
               // tempemployee.DaysWorkable = lawyer.GetResponse("Blah", DaysWorkable);
            }

        }
    }
}
