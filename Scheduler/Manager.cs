using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            List<Employee> employees = Reader.ReadEmployees();
            for(int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {
                if(i > 0)
                {
                    Console.Clear();
                }
                Console.WriteLine(date.DayOfWeek.ToString() + " " + date.Day.ToString());
                List<Employee> workable = Reader.GetWorkableEmployees(date);
                List<Employee> vacationing = Reader.GetVacationingEmployees(date);
                List<Employee> off = Reader.GetOffEmployees(date);
                List<Employee> sick = Reader.GetSickEmployees(date);
                List<Employee> workablelate = Reader.GetWorkableLateEmployees(date);
                List<Employee> scheduled = new List<Employee>();
                List<Employee> scheduledlate = new List<Employee>();
                bool fact = false;
                foreach(Employee employee in employees)
                {
                    if(workable.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0 || off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 || sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0))
                    {
                        scheduled.Add(employee);
                    }
                    else if (workablelate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0 || off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 || sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0))
                    {
                        scheduledlate.Add(employee);
                    }

                }
                foreach (Employee employee in employees)
                {
                     
                    Console.Write(employee.LastName + ": ");
                    if (vacationing.Where(x => x.FirstName==employee.FirstName && x.LastName==employee.LastName).Count() > 0)
                    {
                        Console.Write("V");
                    }
                    else if (off.Where(x => x.FirstName==employee.FirstName && x.LastName==employee.LastName).Count() > 0)
                    {
                        Console.Write("O");
                    }
                    else if (sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                    {
                        Console.Write("S");
                    }
                    else if(scheduled.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                    {
                        Console.Write("R");
                    }
                    else if(scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && !fact)
                    {
                        Console.Write("C");
                        fact = !fact;
                    }
                    Console.ReadLine();
                }
                date = date.AddDays(1.00);
            }
        }

    }
}
