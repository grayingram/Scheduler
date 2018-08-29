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
            List<Employee> employees = Reader.ReadEmployees();
            for(int i = 0; i < DateTime.DaysInMonth(year, month); i++)
            {
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
                    if(workable.Contains(employee) && (!vacationing.Contains(employee) || !off.Contains(employee) || !sick.Contains(employee)))
                    {
                        scheduled.Add(employee);
                    }
                    else if (workablelate.Contains(employee) && (!vacationing.Contains(employee) || !off.Contains(employee) || !sick.Contains(employee)))
                    {
                        scheduledlate.Add(employee);
                    }

                }
                Console.WriteLine("Vacationing employees: " + vacationing.Capacity);
                Console.WriteLine(" Sick employees: " + sick.Capacity);
                Console.WriteLine(" Off employees: " + off.Capacity);
                foreach(Employee employee in employees)
                {
                     
                    //Console.Write(employee.LastName + ": ");
                    if (vacationing.Contains(employee))
                    {
                        Console.Write("V");
                    }
                    else if (off.Contains(employee))
                    {
                        Console.Write("O");
                    }
                    else if (sick.Contains(employee))
                    {
                        Console.Write("S");
                    }
                    else if(scheduled.Contains(employee))
                    {
                        Console.Write("R");
                    }
                    else if(scheduledlate.Contains(employee) && !fact)
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
