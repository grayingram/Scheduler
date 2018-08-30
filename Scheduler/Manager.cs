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
        private Creator Creator { get; set; } = new Creator();
        public void MakeSchedule()
        {
            int year = Lawyer.GetYear("What year is this schedule being made?");
            int month = Lawyer.GetMonth("What month of the year: " + year + " do you want to make the schedule for?");
            DateTime date = new DateTime(year, month, 1);
            string filename = date.Month.ToString() + " " + year.ToString();
            if(Reader.DoesScheduledMonthExist(month, year))
            {
                if(Lawyer.GetYesNo("Do you want to override the current schedule?"))
                {
                    WriteFile(date, filename);
                }
            }
            else
            {
                WriteFile(date, filename);
                Creator.AddScheduledMonth(month, year);
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
        private string LastNameSpacing(string lastname,string symbol)
        {
            string spacing = "";
            for(int i = 0; i < lastname.Length - symbol.Length; i++)
            {
                spacing += " ";
            }
            return spacing;
        }
        private string FormatSpacing(string word)
        {
            string spacing = "";
            for(int i = 0; i < word.Length; i++)
            {
                spacing += " ";
            }
            return spacing;
        }

        private void WriteFile(DateTime date, string filename)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\al_in\source\repos\Scheduler\" + filename + ".txt"))
            {
                List<Employee> employees = Reader.ReadEmployees();
                file.WriteLine(filename);
                string employeesNames = "      ";
                foreach (Employee employee in employees)
                {
                    Console.WriteLine(employee.LastName + ": ");
                    //file.WriteLine(employee.LastName + ": ");
                    employeesNames += employee.LastName + " ";

                }
                Console.ReadLine();
                Console.Clear();
                bool fact = false;
                List<string> daysOfMonth = new List<string>();
                for (int i = 0; i < DateTime.DaysInMonth(date.Year, date.Month); i++)
                {
                    string dayofMonth = "";
                    if (i > 0)
                    {
                        Console.Clear();
                    }
                    Console.WriteLine(FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString());
                    //file.WriteLine(FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString());
                    if (int.Parse(date.Day.ToString()) > 9 && date.DayOfWeek.ToString().ToLower() != "thursday")
                    {
                        dayofMonth += FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString() + " ";
                    }
                    else
                    {
                        dayofMonth += FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString() + "";
                    }
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
                        string symbol = "";
                        dayofMonth += FormatSpacing(FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString());
                        //Console.Write(employee.LastName + ": ");
                        if (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("V\n");
                            symbol = "V";
                            dayofMonth += "V" + LastNameSpacing(employee.LastName, symbol);
                            //file.Write("V");
                        }
                        else if (off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("O\n");
                            symbol = "O";
                            dayofMonth += "O" + LastNameSpacing(employee.LastName, symbol);
                            //file.Write("O");
                        }
                        else if (sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("S\n");
                            symbol = "S";
                            dayofMonth += "S" + LastNameSpacing(employee.LastName, symbol);
                            //file.Write("S");
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && !Reader.HasWorkedLate(employee) && !fact)
                        {
                            Console.Write("C\n");
                            symbol = "C";
                            dayofMonth += "C" + LastNameSpacing(employee.LastName, symbol);
                            //file.Write("C");
                            Updater.UpdateWorkedLateDays(1, Reader.GetEmployeeId(employee));
                            fact = !fact;
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("R\n");
                            symbol = "R";
                            dayofMonth += "R" + LastNameSpacing(employee.LastName, symbol);
                            //file.Write("R");
                        }
                        else if (scheduled.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("R\n");
                            symbol = "R";
                            dayofMonth += "R" + LastNameSpacing(employee.LastName, symbol);
                            //file.Write("R");
                        }
                        else
                        {
                            Console.Write("N_A\n");
                            symbol = "N_A";
                            dayofMonth += "N_A" + LastNameSpacing(employee.LastName, symbol);
                        }

                    }
                    Console.ReadLine();
                    daysOfMonth.Add(dayofMonth);
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
                file.Write(employeesNames);
                file.WriteLine();
                foreach (string dayofmonth in daysOfMonth)
                {
                    file.WriteLine(dayofmonth);
                }
            }
        }

    }
}
