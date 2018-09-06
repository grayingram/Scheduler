using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Scheduler
{/// <summary>
/// Manages existing data into a textfile of a calender month
/// </summary>
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
            string filename = FormatMonth(date.Month.ToString()) + " " + year.ToString();
            if(Reader.DoesScheduledMonthExist(month, year))
            {
                if(Lawyer.GetYesNo("Do you want to override the current schedule of " +FormatMonth(date.Month.ToString()) +" " + year.ToString() +"?"))
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

        /// <summary>
        /// Formats the given day of the week to a one or two length string
        /// </summary>
        /// <param name="day">the day of the week to be formatted</param>
        /// <returns>one or two length representation of the given day</returns>
        private string FormatDay(string day)
        {
            if(day.ToLower() == "monday")
            {
                return "Mon";
            }
            else if (day.ToLower()=="tuesday")
            {
                return "Tue";
            }
            else if (day.ToLower() == "wednesday")
            {
                return "Wed";
            }
            else if (day.ToLower() == "thursday")
            {
                return "Thu";
            }
            else if (day.ToLower() == "friday")
            {
                return "Fri";
            }
            else if (day.ToLower() == "saturday")
            {
                return "Sat";
            }
            return "Sun";
        }

        /// <summary>
        /// Formats the given month to be formatted correctly
        /// </summary>
        /// <param name="month">string of the integer representation of the month</param>
        /// <returns>the name of the month given the string of the integer value of the month</returns>
        private string FormatMonth(string month)
        {
            switch (month)
            {
                case ("1"):
                    {
                        return ("January");
                    }
                case ("2"):
                    return ("Febuary");

                case ("3"):
                    return ("March");
                //break;
                case ("4"):
                    return ("April");
                //break;
                case ("5"):
                    return ("May");
                //break;
                case ("6"):
                    return ("June");
                //break;
                case ("7"):
                    return ("July");
                // break;
                case ("8"):
                    return ("August");
                //break;
                case ("9"):
                    return ("September");
                //break;
                case ("10"):
                    return ("October");
                //break;
                case ("11"):
                    return ("November");
                default:
                    return ("December");
            }
        }

        /// <summary>
        /// Formats a string to be the length of the last name
        /// </summary>
        /// <param name="lastname">lastname of the employee to use</param>
        /// <param name="symbol">as base of how many spaces are needed</param>
        /// <returns>a string with just spacing</returns>
        private string LastNameSpacing(string lastname,string symbol)
        {
            string spacing = "";
            for(int i = symbol.Length; i < lastname.Length; i++)
            {
                spacing += " ";
            }
            return spacing;
        }

        /// <summary>
        /// Formats spacing to the length of the given string
        /// </summary>
        /// <param name="word">word to be used</param>
        /// <returns>spacing the length of the word given</returns>
        private string FormatSpacing(string word)
        {
            string spacing = "";
            for(int i = 0; i < word.Length; i++)
            {
                spacing += " ";
            }
            return spacing;
        }

        /// <summary>
        /// Writes a text file using the database
        /// </summary>
        /// <param name="date">represents the start of the month of a given year</param>
        /// <param name="filename">the name of the file to write to</param>
        private void WriteFile(DateTime date, string filename)
        {
            Console.WriteLine(filename);
            using (StreamWriter file = new StreamWriter(@"C:\Users\al_in\source\repos\Scheduler\" + filename + ".txt"))
            {
                List<Employee> employees = Reader.ReadEmployees();
                file.WriteLine(filename);
                string employeesNamesKey = "Key";
                foreach (Employee employee in employees)
                {
                    employeesNamesKey += employee.LastName + ": " + employee.ID;
                }
                string employeesName = "     ";
                foreach(Employee employee in employees)
                {
                    employeesName += employee.ID + LastNameSpacing(employee.LastName, employee.ID.ToString());
                }
                bool fact = false;
                List<string> daysOfMonth = new List<string>();
                for (int i = 0; i < DateTime.DaysInMonth(date.Year, date.Month); i++)
                {
                    string dayofMonth = "";
                    Console.WriteLine(FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString());
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
                        dayofMonth += FormatSpacing(FormatDay(date.DayOfWeek.ToString()) + "" + date.Day.ToString());
                        Console.Write(employee.LastName + ": ");
                        if (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Vacation\n");
                            dayofMonth += "V";
                        }
                        else if (off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Off\n");
                            dayofMonth += "O";
                        }
                        else if (sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Sick\n");
                            dayofMonth += "S" ;
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && !Reader.HasWorkedLate(employee) && !fact)
                        {
                            Console.Write("Closing\n");
                            dayofMonth += "C";
                            Updater.UpdateWorkedLateDays(1, employee.ID);
                            fact = !fact;
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Working\n");
                            dayofMonth += "R";
                        }
                        else if (scheduled.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Working\n");
                            dayofMonth += "R";
                        }
                        else
                        {
                            Console.Write("NotApplicable\n");
                            dayofMonth += "N_A";
                        }

                    }
                    daysOfMonth.Add(dayofMonth);
                    date = date.AddDays(1.00);
                    fact = !fact;
                    if (date.DayOfWeek.ToString().ToLower() == "saturday")
                    {
                        foreach (Employee employee in employees)
                        {
                            Updater.UpdateWorkedLateDays(employee.ID);
                        }
                    }
                }
                file.WriteLine(employeesNamesKey);
                file.WriteLine(employeesName);
                file.WriteLine();
                foreach (string dayofmonth in daysOfMonth)
                {
                    file.WriteLine(dayofmonth);
                }
            }
        }

    }
}
