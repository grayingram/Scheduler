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
            Console.WriteLine(filename);
            using (StreamWriter file = new StreamWriter(@"C:\Users\al_in\source\repos\Scheduler\" + filename + ".txt"))
            {
                List<Employee> employees = Reader.ReadEmployees();
                file.WriteLine(filename);
                string employeesNamesKey = "Key";
                foreach (Employee employee in employees)
                {
                    //Console.WriteLine(employee.LastName + ": ");
                    employeesNamesKey += employee.LastName + ": " + employee.ID;
                }
                string employeesName = "     ";
                foreach(Employee employee in employees)
                {
                    employeesName += employee.ID + LastNameSpacing(employee.LastName, employee.ID.ToString());
                }
                //Console.ReadLine();
                //Console.Clear();
                bool fact = false;
                List<string> daysOfMonth = new List<string>();
                for (int i = 0; i < DateTime.DaysInMonth(date.Year, date.Month); i++)
                {
                    string dayofMonth = "";
                    if (i > 0)
                    {
                        //Console.Clear();
                    }
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
                        //string symbol = "";
                        dayofMonth += FormatSpacing(FormatDay(date.DayOfWeek.ToString()) + "\n" + date.Day.ToString());
                        Console.Write(employee.LastName + ": ");
                        if (vacationing.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Vacation\n");
                            //symbol = "V";
                            dayofMonth += "V";
                        }
                        else if (off.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Off\n");
                            //symbol = "O";
                            dayofMonth += "O";
                        }
                        else if (sick.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Sick\n");
                            //symbol = "S";
                            dayofMonth += "S" ;
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0 && !Reader.HasWorkedLate(employee) && !fact)
                        {
                            Console.Write("Closing\n");
                            //symbol = "C";
                            dayofMonth += "C";
                            Updater.UpdateWorkedLateDays(1, employee.ID);
                            fact = !fact;
                        }
                        else if (scheduledlate.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Working\n");
                            //symbol = "R";
                            dayofMonth += "R";
                        }
                        else if (scheduled.Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).Count() > 0)
                        {
                            Console.Write("Working\n");
                            //symbol = "R";
                            dayofMonth += "R";
                        }
                        else
                        {
                            Console.Write("NotApplicable\n");
                            //symbol = "N_A";
                            dayofMonth += "N_A";
                        }

                    }
                    //Console.ReadLine();
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
