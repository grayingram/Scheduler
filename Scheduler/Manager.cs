using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Scheduler
{
    class Manager
    {
        private Lawyer Lawyer { get; set; } = new Lawyer();
        private Reader Reader { get; set; } = new Reader();
        private Updater Updater { get; set; } = new Updater();
        public void MakeSchedule()
        {
            int year = Lawyer.GetYear("What year is this schedule being made?");
            int month = Lawyer.GetMonth("What month of the year: " + year + " do you want to make the schedule for?");
            DateTime date = new DateTime(year, month, 1);
            string filename = date.Month.ToString() + " " + year.ToString();
            using (StreamWriter file = new StreamWriter(@"C:\Users\al_in\source\repos\Scheduler\" + filename+".txt")) {
                List<Employee> employees = Reader.ReadEmployees();
                foreach (Employee employee in employees)
                {
                    Console.WriteLine(employee.LastName + ": ");
                    file.WriteLine(employee.LastName + ": ");
                }
                bool fact = false;
                for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
                {
                    if (i > 0)
                    {
                        Console.Clear();
                    }
                    Console.WriteLine(FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString());
                    file.WriteLine(FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString());
                    List<Employee> workable = Reader.GetWorkableEmployees(date);
                    List<Employee> vacationing = Reader.GetVacationingEmployees(date);
                    List<Employee> off = Reader.GetOffEmployees(date);
                    List<Employee> sick = Reader.GetSickEmployees(date);
                    List<Employee> workablelate = Reader.GetWorkableLateEmployees(date);
                    List<Employee> scheduled = new List<Employee>();
                    List<Employee> scheduledlate = new List<Employee>();

                    foreach (Employee employee in employees)
                    {
                        if (workablelate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0 || off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0 || sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0))
                        {
                            scheduledlate.Add(employee);
                        }
                        else if (workable.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0 || off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0 || sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() == 0))
                        {
                            scheduled.Add(employee);
                        }


                    }
                    foreach (Employee employee in employees)
                    {

                        //Console.Write(employee.LastName + ": ");
                        if (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("V");
                            file.Write("V");
                        }
                        else if (off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("O");
                            file.Write("O");
                        }
                        else if (sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("S");
                            file.Write("S");
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && !Reader.HasWorkedLate(employee) && !fact)
                        {
                            Console.Write("C");
                            file.Write("C");
                            Updater.UpdateWorkedLateDays(1, Reader.GetEmployeeId(employee));
                            fact = !fact;
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("R");
                            file.Write("R");
                        }
                        else if (scheduled.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("R");
                            file.Write("R");
                        }
                        Console.ReadLine();
                    }
                    date = date.AddDays(1.00);
                    fact = !fact;
                    if (date.DayOfWeek.ToString().ToLower() == "saturday")
                    {
                        foreach (Employee employee in employees)
                        {
                            Updater.UpdateWorkedLateDays(Reader.GetEmployeeId(employee));
                        }
                    }
                }
            }
        }

        private string FormatDay(string day)
        {
            if(day.ToLower() == "monday")
            {
                return "M";
            }
            else if (day.ToLower()=="tuesday")
            {
                return "T";
            }
            else if (day.ToLower() == "wednesday")
            {
                return "W";
            }
            else if (day.ToLower() == "thursday")
            {
                return "Th";
            }
            else if (day.ToLower() == "friday")
            {
                return "F";
            }
            return "S";
        }

    }
}
